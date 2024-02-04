unit bcParser;
{
  Copyright Suavi Ali Demir, 1999-2012
                                                                                                                
  www.bestcode.com
  support@bestcode.com

  TbcParser VCL Component
  Mathematical String Expression Parser

Pre-XE version

  version 4.3 - June 2015
    Big Fix: Memory leak leftNode.Free (Thanks Steven) 
  version 4.0 - May 2015
    Bug Fix: Memory leak in GetVariablesUsed and TbcParserEx.
  version 3.9 - June 2014
    Bug Fix: Using N param function leak memory due to missing params.Free (Thanks Bill!)
  version 3.8 - May 2012
    Bug Fix: Bug fix - if statement not operator precedence needs to be enforced by paranthesis.
	not Length(param)>0 ---> not (Length(param)>0)
  version 3.7 - July 2010
    Bug Fix: A-E-1 results in wrong value. Val procedure thinks E-1 is scientific notation.
  version 3.6 - June 2010
    GetVariablesUsed function to return variables used in the expression.
  version 3.5
    OnVariableFound event to provide variable values on demand instead of pre-defining them.
  version 3.4
    Bug Fix: Space in "(((2-x)*3) )" was not accounted for.
  version 3.3
    Bug Fix: 2*-1 works but 2* -1 does not. space caused problem.
    Bug Fix: sin(x) works but sin  (x) does not. space caused problem.

  version 3.2
	Bug Fix: Anomaly introduced in previous fix where an expression with paranthesis
	like ( -(x+1)) is being rejected as invalid and requires (-(x+1)). 
	The leading space before - sign needs to be handled. This happens
	if the expression portion is in paranthesis only.

  version 3.1
    Better support for error messages where invalid portion can be communicated to the caller.

  version 3.0 February 2005
    Sample Support for string constants.

  version 2.8 October 2004
    Bug Fix: If I hand the parser 1E-2*1E-3  I get a syntax error.
    If I use (1E-2)*(1E-3) then its ok.

  version 2.7 September 2004
    Fixed syntax error bug that appeared in some platforms due to the following anomaly:
      temp_[0] := 'a';
      temp_[1] := 'b';
      temp_[2] := 'c';
      temp_[3] := chr(0);
      Windows.MessageBox(0, PChar('We are looking for '''+string(temp_) +''''), 'Test', MB_OK);

      You would expect that in the MessageBox you would see : 'abc' but you see 'abc
      (Last quote missing) Seems to be Borland bug which we need to side step.

  version 2.6 November 2003
    IsNParamFunction function added.
  version 2.5 August 2003
    Fixed: TbcParser.Randomize stack overflow due to undesired recursion.

  version 2.4 May 2003
    Fixed: Expression like '7--' causing access violation.

  version 2.3 March 2003
    Logical operators <=, >=, <>.

  version 2.2 Feb  2003
    Memory leak in destroy fixed.
    
  version 2.1 July 2002
    Logical operators are now supported: <, >, |, &, =, !

  version 2.0 March 2002
    Functions with N number of parameters are supported.

    IF(A,B,C) is added to standard function list.

    Function and variable name lenghts are checked, or it could crash with names >255 chars.

    Variable and Function names now support A-Z, _, 0-9 chars.

    CreateVar no longer throws exception if variable already exists,
    assigns new value instead.

    AssignVar no longer throws exception if variable does not exist, it creates
    it first and then assigns it's value.


  version 1.01 July 2000: added following functions to the public interface:
    function IsVariable(const varName: string) : boolean;
    function IsOneParamFunction(const funcName: string): boolean;
    function IsTwoParamFunction(const funcName: string): boolean;

  Added <$ObjExportAll On> compiler directive to allow the component to be used
  with C++ Builder with run time packages.

  Centralized optimization code in OptimizeNode(var aNode: TNode) procedure.

  Made the temp: array[256] of char a member variable instead of a global array
  not to have multithreading problems regarding synchronization of global data access.

  Removed const ALPHA : set of char = ['A'..'Z']
  and used character comparison instead of set operation to improve speed.
}

//{$DEFINE EVALVERSION}
//{$DEFINE _debug}

{$ObjExportAll On}

interface

uses
  SysUtils, Classes;

type
    NUMBER = extended; //the number format used in the calculations
    PNUMBER = ^NUMBER;


const
  MAX_NAME_LEN = 255;


type
  EbcParserError = class(Exception)
    private
      FExp : String; //expression that caused error.
      FErr : String; //The portion of expression that causes error.
    public
      constructor Create(const Msg : String; const ErrPortion : String; const Exp : String);
      function GetInvalidPortionOfExpression: String; virtual;
      function GetSubExpression: String; virtual;
  end; // create a new exception class

type
  { TTwoParamFunc type specifies the prototype of the functions that users can
    add to the list of available functions with two parameters to be used in
    an expression. }
  TTwoParamFunc = function(const x: NUMBER; const y: NUMBER) : NUMBER;
  { TOneParamFunc type specifies the prototype of the functions that users can
    add to the list of available functions with one parameter to be used in an expression. }
  TOneParamFunc = function(const x: NUMBER) : NUMBER;
  { TNParamFunc type specifies the prototype of the functions that users can
    add to the list of available functions with n number of parameters
    to be used in an expression. }
  TNParamFunc = function(const x: array of NUMBER) : NUMBER;

  { TOnVariableFoundEvent specifies the event that users can
    implement to return the value of a variable that was not defined before parse time.
    This may typically be needed when the application's domain is so large that it is
    not possible to predefine all possible variables. If OnVariableFound event handler is assigned,
    then the parser will call this function to obtain variable values that are used
    in the expression.
    The first parameter Sender is the TbcParser instance.
    The second parameter varName is the name of the variable whose value is being asked for.
  }
  TOnVariableFoundEvent = procedure(Sender : TObject; const varName : String; var RetVal : NUMBER) of Object;

type //generic node, base for all nodes that are declared in implementation section
  TNode = class(TObject)
    public
    function GetValue: NUMBER; virtual; abstract;
    function IsUsed(Addr: Pointer): boolean; virtual; abstract;
    //Optimize evaluates constant values at compile time.
    procedure Optimize; virtual; abstract;
    //destructor Destroy; override; abstract; //no need to declare this since it does not do anything.
  end;
  //PNode = ^TNode;
  PDoubleArray = ^TDoubleArray;
  TDoubleArray = array[0..MaxListSize-1] of NUMBER;

type
  {
    This mathematical expression parser component parses and evaluates a
    mathematical expression that may contain variables and functions.

    Examples to typical expressions are:

      'SIN(3.14)+5^2+POW(2,7)-MAX(10,20)'

      '( [ SIN(X)^2 ] + COS(1) ) * PI/2'

      '( COSH(2.71)+ SINH( COS(0.5) * POW(0.2345, 0.67) )) - LOG(1-X^2+X^5)'

      '(90+292*POW(X,0.31))*(1+0.025*LOG(Y))*(1-1*POW(Y+X,1.09))'

      '25993.894*EXP(-1.53389*(X-32))*POW(X, 0.13547)*POW(Y, 0.38603)*EXP(-0.36222*Y)'


      The user can add/remove his/her own custom variables and functions to be used
      in the expression. To be efficient in repeated calculations, parser creates
      a parse tree at first and reuses this parse tree for each evaluation without the
      need to reparse.

      If Optimization is on, the parse tree will be optimized by calculating constant
      expression sections at once so that further evaluation requests will be quicker.
  }
  TbcParser = class(TComponent)
  private
    { Private declarations }
    //temp char array is a member variable so that different threads
    //instantiating different instances of this parser object donot cause problem
    //sharing a global array.
    //temp_ : array [1..MAX_NAME_LEN] of char; //static array for temporary usage

    //FDirty indicates if the expressions has been changed since last compile.
    FDirty         : boolean;
    //FNode is the tree structure that holds parsed elements that form the formula.
    FNode          : TNode;

    //FVariables is the list that holds variables (as string) and their values (as object pointer)
    FVariables     : TStringList;

    //FOneParamFuncs is the list of function names and their function
    //addresses(pointers) (such as Sin(X), Cos(X, Log(X)) etc)
    FOneParamFuncs : TStringList;

    { FTwoParamFuncs is the list of function names and their function
      addresses (Power(X, Y), Min(A, B) etc) }
    FTwoParamFuncs : TStringList;

    { FNParamFuncs is the list of function names and their function
      addresses (IF(A, B, C) etc) }
    FNParamFuncs : TStringList;

    FOptimizationOn: boolean;

    { If assigned, this is used to resolve variable values that are not defined before parse }
    FOnVariableFound : TOnVariableFoundEvent;
  protected
    //FExpression is the string expression to be parsed. This is user input.
    //To work on, the parse function creates a copy of this expression so that
    //the original is not changed.
    FExpression    : string; //need to use from derived class.

    function GetExpression : string;
    procedure SetExpression(const str : string); virtual;
    procedure SetX(const x : NUMBER);
    function GetX : NUMBER;
    procedure SetY(const y : NUMBER);
    function GetY : NUMBER;

    function FindLastOper(const formula: string) : integer;

    function CreateParseTree(const formulaText: string; var Node: TNode) : boolean;
    function IsTwoParamFunc( const formula : string;
                         var paramLeft, paramRight: string;
                         var funcAddr : TTwoParamFunc;
                         CurrChar : integer ) : boolean;
    function IsOneParamFunc( const formula : string;
                         var param: string;
                         var funcAddr : TOneParamFunc;
                         CurrChar : integer ) : boolean;

    function IsNParamFunc( const formula : string;
                         var params: TStringList;
                         var funcAddr : TNParamFunc
                         ) : boolean;

    procedure Optimize;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    { Parses the expression and forms a parse tree. Throws EbcParserError
      exception if it cannot parse. Upon successful completion of parsing, it
      will set the Dirty flag to false, so that unless the expression is
      changed it does not need to re-parsed. Users may want to call the parse
      method directly to check the validity of an input expression using a
      try-except block.

      If OptimizationOn property is true, Parse method will optimize the parse
      tree by evaluating constant branches of the parse tree at this moment, so
      that Evaluate function will run faster. }
    procedure Parse; virtual;
    { Evaluate function returns the numerical value of the mathematical string
      Expression. If the expression was not parsed before, it will be parsed
      for the evaluation. Once the expression is parsed, it won't be parsed
      again and again to obtain the numeric value. Instead the output value
      will be efficiently calculated from the parse tree. If the Expression property
      is re-assigned a new mathematical expression, then expression will be
      parsed next time the Evaulate function or Parse method is called. }
    function Evaluate : NUMBER; virtual;

    { CreateVar method creates a new variable in the parser's list of variables.
      If the variable name already exists, CreateVar throws EbcParserError exception. }
    procedure CreateVar(const name: string; const value: NUMBER); virtual;

    { CreateOneParamFunc method creates a new function that takes one parameter
      in the parser's list of functions. If the function name already exists as
      a one parameter or two parameter function then CreateOneParamFunc throws
      EbcParserError exception. }
    procedure CreateOneParamFunc(const name: string; procAddr: TOneParamFunc); virtual;

    { CreateTwoParamFunc method creates a new function that takes two parameters
      in the parser's list of functions. If the function name already exists as
      a one parameter or two parameter function then CreateTwoParamFunc throws
      EbcParserError exception. }
    procedure CreateTwoParamFunc(const name: string; procAddr: TTwoParamFunc); virtual;

    { CreateNParamFunc method creates a new function that takes n>2 parameters
      in the parser's list of functions. If the function name already exists as
      a function then CreateNParamFunc throws EbcParserError exception. nParam
      is the number of parameters which is also the size of the array that
      TNParamFunc takes as parameter. }
    procedure CreateNParamFunc(const name: string; procAddr: TNParamFunc; nParam : Integer); virtual;

    { CreateDefaultFuncs method creates some predefined functions in the
      parser's list of functions.

      Predefined functions that take one parameter are:

      SQR: Square function which can be used as SQR(X)

      SIN: Sinus function which can be used as SIN(X), X is a real-type expression. Sin returns the sine of the angle X in radians.

      COS: Cosinus function which can be used as COS(X), X is a real-type expression. COS returns the cosine of the angle X in radians.

      ATAN: ArcTangent function which can be used as ATAN(X)

      SINH: Sinus Hyperbolic function which can be used as SINH(X)

      COSH: Cosinus Hyperbolic function which can be used as COSH(X)

      COTAN: which can be used as COTAN(X)

      TAN: which can be used as TAN(X)

      EXP: which can be used as EXP(X)

      LN: natural log, which can be used as LN(X)

      LOG: 10 based log, which can be used as LOG(X)

      SQRT: which can be used as SQRT(X)

      ABS: absolute value, which can be used as ABS(X)

      SIGN: SIGN(X) returns -1 if X<0; +1 if X>0, 0 if X=0; it can be used as SQR(X)

      TRUNC: Discards the fractional part of a number. e.g. TRUNC(-3.2) is -3, TRUNC(3.2) is 3.

      CEIL: CEIL(-3.2) = 3, CEIL(3.2) = 4

      FLOOR: FLOOR(-3.2) = -4, FLOOR(3.2) = 3

      RND:  Random number generator.

            RND(X) generates a random INTEGER number such that 0 <= Result < int(X).
            Call TbcParser.Randomize to initialize the random number generator
            with a random seed value before using RND function in your expression.

      RANDOM: Random number generator.

            RANDOM(X) generates a random floating point number such that 0 <= Result < X.
            Call TbcParser.Randomize to initialize the random number generator
            with a random seed value before using RANDOM function in your expression.

      Predefined functions that take two parameters are:

      INTPOW: The INTPOW function raises Base to an integral power. INTPOW(2, 3) = 8.
              Note that result of INTPOW(2, 3.4) = 8 as well.

      POW: The Power function raises Base to any power. For fractional exponents
          or exponents greater than MaxInt, Base must be greater than 0.

      LOGN: The LogN function returns the log base N of X. Example: LOGN(10, 100) = 2

      MIN: MIN(2, 3) is 2.

      MAX: MAX(2, 3) is 3.

      Predefined functions that take more than 2 parameters are:

      IF: IF(BOOL, X, Y) returns X if BOOL is <> 0, returns Y if BOOL =0.
      Values of X and Y are calculated regardless of BOOL (Full Boolean Evaluation).
    }
    procedure CreateDefaultFuncs; virtual;
    { X, Y and PI variables are predefined and can be immediately used in the
      expression. Initial values of X and Y are 0. PI is 3.14159265358979 }
    procedure CreateDefaultVars; virtual;

    { AssignVar method assigns a given value 'val' to the variable named 'name'. }
    procedure AssignVar(const name: string; val: NUMBER); virtual;

    { GetVar method returns the current value of the variable named 'name'.}
    function GetVar(const name: string): NUMBER; virtual;

    { DeleteVar method deletes an existing variable from the list of available
      variables. If the variable does not exist, then DeleteVar throws
      EbcParserError exception.

      When a variable is deleted  Dirty flag is set to true so that next time
      the Evaluate function is called the expression will be reparsed. }
    procedure DeleteVar(const name: string); virtual;

    { DeleteFunc method deletes an existing function from the list of available
      functions. If the function does not exist, then DeleteVar throws
      EbcParserError exception.

      When a function is deleted  Dirty flag is set to true so that next time
      the Evaluate function is called the expression will be reparsed. }
    procedure DeleteFunc(const name: string); virtual; //one param or two param, does not matter.

    { DeleteAllVars method deletes all variables from the list of available
      variables.

      This action may be useful when number of unused variables is too high that
      causes performance to degrade.

      When a variable is deleted  Dirty flag is set to true so that next time
      the Evaluate function is called the expression will be reparsed. }
    procedure DeleteAllVars; virtual;

    { DeleteAllFuncs method deletes all variables from the list of available
      functions.

      This action may be useful when number of unused functions is too high that
      causes performance to degrade.

      When a function is deleted  Dirty flag is set to true so that next time
      the Evaluate function is called the expression will be reparsed. }
    procedure DeleteAllFuncs; virtual;

    {
      Returns a new TStringList object that contains all available function names.
      It is caller's responsibility to free the returned TStringList.
      Making changes to this list does not effect the parser.
    }
    function GetFunctions: TStringList; virtual;

    {
      Returns a new TStringList object that contains all available variable names.
      It is caller's responsibility to free the returned TStringList.
      Making changes to this list does not effect the parser.
    }
    function GetVariables: TStringList; virtual;

    {
      Returns a new TStringList object that contains variable names used in
      the current expression.
      It is caller's responsibility to free the returned TStringList.
      Making changes to this list does not effect the parser.
    }
    function GetVariablesUsed: TStringList; virtual;

    { Randomize function should be called to initialize the predefined RND or
      RANDOM functions if the expression uses them. }
    procedure Randomize; virtual;

    { FreeParseTree can be explicitly called to free the resources taken by the
      allocated Parse tree when an expression is parsed. FreeParseTree sets the
      Dirty flag to true so that next time the Evaluate function is called,
      expression will be parsed forming a new, valid parse tree to be evaluated. }
    procedure FreeParseTree; virtual;

    { Returns true if a variable with the name 'varName' is used in the current
      expression. }
    function IsVariableUsed(const varName: string): boolean; virtual;

    { Returns true if a function with the name 'funcName' is used in the current
      expression. }
    function IsFunctionUsed(const funcName: string): boolean; virtual;

    { Value property is an intuitive way of retrieving the value of the
      input expression. Value property is a read only property which is in
      fact just an alias for the Evaluate method. }
    property Value : NUMBER read Evaluate;
    { Variable property is a way to set and get variable values. }
    property Variable[const name: string] : NUMBER read GetVar write AssignVar;

    { some useful functions: }
    { Returns true if a variable with the name 'varName' is present in the current
      variables list. }
    function IsVariable(const varName: string) : boolean;
    { Returns true if a function with the name 'funcName' is present in the current
      one parameter functions list. }
    function IsOneParamFunction(const funcName: string): boolean;
    { Returns true if a function with the name 'funcName' is present in the current
      two parameter functions list. }
    function IsTwoParamFunction(const funcName: string): boolean;
    { Returns true if a function with the name 'funcName' is present in the current
      n parameter functions list. }
    function IsNParamFunction(const funcName: string): boolean;
    { Returns true if a function with the name 'funcName' is already registered as a
      function that takes one or two or n number of parameters. }
    function IsFunction(const funcName: string): boolean;

  published
    {
      Expression property represents the mathematical expression which is input
      to be evaluated by the user.

      The expression can contain variables such as X, Y, T, HEIGHT, WEIGHT and so on.
      Expression can also contain functions that take one parameter, two or more parameters.
      Expression can contain variables that are not pre-defined if OnVariableFound event handler
      is assigned.

      When Expression is assigned a value, it becomes 'dirty' and further attempt
      to evaluate its value will require it to be parsed. But once it is parsed,
      and a parse tree representing the expression is formed, it won't be parsed
      again, until it is assignd a new string. Instead, the parse tree will be used to
      retrieve current results as the values of variables change.

      X, Y and PI variables are predefined and can be immediately used in the expression.
      CreateVar method can be used to add user variables.

      Predefined functions that take one parameter are:
      SQR: Square function which can be used as SQR(X)
      SIN: Sinus function which can be used as SIN(X), X is a real-type expression. Sin returns the sine of the angle X in radians.
      COS: Cosinus function which can be used as COS(X), X is a real-type expression. COS returns the cosine of the angle X in radians.
      ATAN: ArcTangent function which can be used as ATAN(X)
      SINH: Sinus Hyperbolic function which can be used as SINH(X)
      COSH: Cosinus Hyperbolic function which can be used as COSH(X)
      COTAN: which can be used as COTAN(X)
      TAN: which can be used as TAN(X)
      EXP: which can be used as EXP(X)
      LN: natural log, which can be used as LN(X)
      LOG: 10 based log, which can be used as LOG(X)
      SQRT: which can be used as SQRT(X)
      ABS: absolute value, which can be used as ABS(X)
      SIGN: SIGN(X) returns -1 if X<0; +1 if X>0, 0 if X=0; it can be used as SQR(X)
      TRUNC: Discards the fractional part of a number. e.g. TRUNC(-3.2) is -3, TRUNC(3.2) is 3.
      CEIL: CEIL(-3.2) = 3, CEIL(3.2) = 4
      FLOOR: FLOOR(-3.2) = -4, FLOOR(3.2) = 3
      RND:  Random number generator.
            RND(X) generates a random INTEGER number such that 0 <= Result < int(X).
            Call TbcParser.Randomize to initialize the random number generator
            with a random seed value before using RND function in your expression.
      RANDOM: Random number generator.
            RANDOM(X) generates a random floating point number such that 0 <= Result < X.
            Call TbcParser.Randomize to initialize the random number generator
            with a random seed value before using RANDOM function in your expression.

      Predefined functions that take two parameters are:
      INTPOW: The INTPOW function raises Base to an integral power. INTPOW(2, 3) = 8.
              Note that result of INTPOW(2, 3.4) = 8 as well.
      POW: The Power function raises Base to any power. For fractional exponents
          or exponents greater than MaxInt, Base must be greater than 0.
      LOGN: The LogN function returns the log base N of X. Example: LOGN(10, 100) = 2
      MIN: MIN(2, 3) is 2.
      MAX: MAX(2, 3) is 3.

      Predefined functions that take more than 2 parameters are:

      IF: IF(BOOL, X, Y) returns X if BOOL is <> 0, returns Y if BOOL =0.
      Values of X and Y are calculated regardless of BOOL (Full Boolean Evaluation).

      User functions can be added using CreateOneParamFunc, CreateTwoParamFunc and CreateNParamFunc methods.
      Functions and Variables can be deleted using DeleteVar, DeleteFunc, DeleteAllVars, DeleteAllFuncs methods.
    }

    property Expression : string read GetExpression write SetExpression;

    {
      X property represents the X variable used in the mathematical expression which
      was input to be evaluated. You can set the X variable to a numeric value and
      call the Parse method (or Value property) to retrieve the new result of the expression.
      X variable is created by default for the convenience of the user.
      Additional variables can be added by using the CreateVar method.
      Variable names are case insensitive.
    }
    property X : NUMBER read GetX write SetX;

    {
      Y property represents the Y variable used in the mathematical expression which
      was input to be evaluated. You can set the Y variable to a numeric value and
      call the Parse method (or Value property) to retrieve the new result of the expression.
      Y variable is created by default for the convenience of the user.
      Additional variables can be added by using the CreateVar method.
      Variable names are case insensitive.
    }
    property Y : NUMBER read GetY write SetY;

    {
      Set OptimizationOn to let the bcParser component evaluate constant
      expressions at parse time. The optimized parse tree will enhance
      subsequant evaluation operations, though initial parsing will be slower.

      Optimization is good if you are going to parse once and evaluate the same
      expression many many times with different variable values.

      When OptimizationOn is true, following code runs in 170 milliseconds.
      When Optimization is false, following code runs in 220 milliseconds on PII 300.

      procedure TForm1.bbCalcClick(Sender: TObject);
      var i: integer;
          count: integer;
      begin
        bcParser.Expression := 'SIN(3.14)+5^2+POW(2,7)-MAX(10,20)';

        count:= GetTickCount;
        try
          for i:= 1 to 10000 do
          begin
             bcParser.X := i*2;
             bcParser.Y := i/2;
             labResult.Caption := FloatToStr(bcParser.Parse);
           end;
        except
           on e: Exception do
           ShowMessage(e.Message);
        end;

        ShowMessage(FloatToStr(GetTickCount - count));
      end;
    }
    property OptimizationOn: boolean read FOptimizationOn write FOptimizationOn default true;

    {
      If OnVariableFound is not assigned, TbcParser requires that all variables that the parser uses are predefined
      before the parse operation, or an expcetion is thrown indicating syntax error.
      If OnVariableFound is defined, then TbcParser tolerates undefined variables in the expression and during
      evaluation of the formula, TbcParser calls the OnVariableFound event handler to obtain the value of the
      variable. The user's implementation of OnVariableFound can decide whether it is a valid variable or not and what the
      value shall be.
      This is typically useful in cases when the application domain is so big that it is not possible to predefine
      variables ahead of time.
    }
    //TODO: Should we reset the FDirty flag when VariableResolver is assigned?
    property OnVariableFound : TOnVariableFoundEvent read FOnVariableFound write FOnVariableFound;
  end;


type
  TbcParserEx = class(TbcParser)
  private
   {Strings used in the expression are placed in this table before actual parsing,
    and their location index in this table is used in the actual expression.
    String support functions provide mapping between the actual indexes and their
    location indexes within the expression. These details are hidden to the end user.}
    FStringTable     : TStringList;

    //used to make this parser instance accessible from global user functions.
    //such functions are not member functions and may need a way to access the parser instance
    //that is invoking them.
    FParserIndex     : Integer;

  protected
    //function GetExpression : string;
    //procedure SetExpression(const str : string);
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;

    { This is overriden to create the default string support functions.
    }
    procedure Parse; override;
    procedure CreateDefaultFuncs; override;
    procedure CreateDefaultVars; override;
    procedure SetExpression(const str: string); override;
  published
    property StringTable : TStringList read FStringTable write FStringTable;
  end;


{This function is used to map the passed in parameters to the actual string constant that was used.}
function ToParsedStr(const x,y: NUMBER): String;

procedure Register;


implementation

uses math{, strutils}    {$IFDEF EVALVERSION}, windows {$ENDIF};

//global List that holds the TbcParserEx instances and makes them reachable from the global
//user defined functions. These need to be properly protected in a multithreaded application.
var
  ParserList : TList = nil;
  ParserCount: Integer = 0;

threadvar
  VariableToSearchFor : String; //a hack to implement TUnknownVarNode.IsUsed

type //a record that ties a functions memory address to the number of parameters it takes.
  TNParamFuncRecord = record
    Func    : TNparamFunc;
    nParam  : Integer;
  end;
  PNParamFuncRecord = ^TNParamFuncRecord; 


function IsValidChar(index : integer; c : char):boolean;
begin
    if (index=1) then begin //if first index
      if ((c>='A') and (c<='Z')) or (c='_') then
        Result:= true
      else
        Result:= false;
    end else
    begin
        if ((c>='A') and (c<='Z')) or ((c>='0') and (c<='9')) or (c='_') then
          Result:= true
        else
          Result:= false;
    end;
end;

function IsValidName(const name : string):boolean;
  var i, len: integer;
begin
  len := Length(name);
  if len>0 then begin
    for i:= 1 to len do begin
      if not IsValidChar(i, name[i]) then begin
        Result := false;
        exit;
      end;
    end;
  end;
  Result := true;
end;

procedure DisposeList(var List : TList);
  var i: Integer;
begin
  for i:= List.Count-1 downto 0 do
  begin
   Dispose(List.Items[i]);
   List.Delete(i);
  end;
  List.Free;
  List := nil;
end;

procedure OptimizeNode(var aNode: TNode); forward;

type //constants such as 3, 5 7 in the formula
  TBasicNode = class(TNode)
  private
    Value : NUMBER;
  public
    function GetValue: NUMBER; override;
    function IsUsed(Addr: Pointer): boolean; override;
    procedure Optimize; override; //Optimize evaluates constant values at compile time.
    constructor Create(Val : NUMBER);
    destructor Destroy; override;
  end;

type //One parameter functions such as SIN(X) etc
  TOneParamNode = class(TNode)
  private
    Child: TNode;
    Func : TOneParamFunc;
  public
    function GetValue: NUMBER; override;
    function IsUsed(Addr: Pointer): boolean; override;
    procedure Optimize; override; //Optimize evaluates constant values at compile time.
    constructor Create(ChildNode: TNode; FuncAddr : TOneParamFunc);
    destructor Destroy; override;
  end;

type //Two parameter functions such as POWER(X, Y) or 3+5 etc
  TTwoParamNode = class(TNode)
  private
    Left, Right: TNode;
    Func : TTwoParamFunc;
  public
    function GetValue: NUMBER; override;
    function IsUsed(Addr: Pointer): boolean; override;
    procedure Optimize; override; //Optimize evaluates constant values at compile time.
    constructor Create(LeftNode, RightNode: TNode; FuncAddr : TTwoParamFunc);
    destructor Destroy; override;
  end;

type //N parameter functions such as IF(X,Y,Z) etc
  TNParamNode = class(TNode)
  private
    Children: TList;
    Func : TNParamFunc;
  public
    function GetValue: NUMBER; override;
    function IsUsed(Addr: Pointer): boolean; override;
    procedure Optimize; override; //Optimize evaluates constant values at compile time.
    constructor Create(var Nodes: TList; FuncAddr : TNParamFunc);
    destructor Destroy; override;
  end;

type //a variable node
  TVarNode = class(TNode)
  private
    pVar    : PNUMBER;   //address of the variable in the variable list
  public
    function GetValue: NUMBER; override;
    function IsUsed(Addr: Pointer): boolean; override;
    procedure Optimize; override; //Optimize evaluates constant values at compile time.
    constructor Create(variable: PNUMBER);
    destructor Destroy; override;
  end;

type //a variable node
  TUnknownVarNode = class(TNode)
  private
    VarName    : String; //variable name.
    MathParser : TbcParser; //the parser that is using this variable now.
  public
    function GetValue: NUMBER; override;
    function IsUsed(Addr: Pointer): boolean; override;
    procedure Optimize; override; //Optimize evaluates constant values at compile time.
    constructor Create(Parser : TbcParser; VariableName: String);
    destructor Destroy; override;
  end;

////////////////////////////////////////////////////////////////////////////////
//Functions that the parser accepts and uses
//Users can also assign their own functions
function _greater(const x,y: NUMBER): NUMBER;
begin
     if (x>y) then Result:= 1
     else Result:= 0;
end;
//------------------------------------------------------------------------------
function _less(const x,y: NUMBER): NUMBER;
begin
     if (x<y) then Result:= 1
     else Result:= 0;
end;
//------------------------------------------------------------------------------
function _equal(const x,y: NUMBER): NUMBER;
begin
     if (x=y) then Result:= 1
     else Result:= 0;
end;
//------------------------------------------------------------------------------
function _ltequals(const x,y: NUMBER): NUMBER;
begin
     if (x<=y) then Result:= 1
     else Result:= 0;
end;
//------------------------------------------------------------------------------
function _gtequals(const x,y: NUMBER): NUMBER;
begin
     if (x>=y) then Result:= 1
     else Result:= 0;
end;
//------------------------------------------------------------------------------
function _notequals(const x,y: NUMBER): NUMBER;
begin
     if (x<>y) then Result:= 1
     else Result:= 0;
end;
//------------------------------------------------------------------------------
function _and(const x,y: NUMBER): NUMBER;
begin
     if ((x<>0) and (y<>0)) then Result:= 1
     else Result:= 0;
end;
//------------------------------------------------------------------------------
function _or(const x,y: NUMBER): NUMBER;
begin
     if ((x<>0) or (y<>0)) then Result:= 1
     else Result:= 0;
end;
//------------------------------------------------------------------------------
function _not(const x: NUMBER): NUMBER;
begin
     if (x=0) then Result:= 1
     else Result:= 0;
end;

//------------------------------------------------------------------------------
function _unaryadd(const x: NUMBER): NUMBER;
begin
     Result:= x;
end;
//------------------------------------------------------------------------------
function _add(const x,y: NUMBER): NUMBER;
begin
    Result := x + y;
end;
//------------------------------------------------------------------------------
function _subtract(const x,y: NUMBER): NUMBER;
begin
    Result := x - y;
end;
//------------------------------------------------------------------------------
function _multiply(const x,y: NUMBER): NUMBER;
begin
    Result := x * y;
end;
//------------------------------------------------------------------------------
function _divide(const x,y: NUMBER): NUMBER;
begin
    Result := x / y;
end;
//------------------------------------------------------------------------------
function _modulo(const x,y: NUMBER): NUMBER;
begin
    Result := trunc(x) mod trunc(y);
end;
//------------------------------------------------------------------------------
function _intdiv(const x,y: NUMBER): NUMBER;
begin
    Result := trunc(x) div trunc(y);
end;
//------------------------------------------------------------------------------
function _negate(const x: NUMBER): NUMBER;
begin
    Result := -x;
end;
//------------------------------------------------------------------------------
function _intpower(const x,y: NUMBER): NUMBER;
begin
    Result := IntPower(x, trunc(y));
end;
//------------------------------------------------------------------------------
function _square(const x: NUMBER): NUMBER;
begin
    Result := sqr(x);
end;
//------------------------------------------------------------------------------
function _power(const x,y: NUMBER): NUMBER;
begin
    Result := Power(x, y);
end;
//------------------------------------------------------------------------------
function _sin(const x: NUMBER): NUMBER;
begin
    Result := sin(x);
end;
//------------------------------------------------------------------------------
function _cos(const x: NUMBER): NUMBER;
begin
    Result := cos(x);
end;
//------------------------------------------------------------------------------
function _arctan(const x: NUMBER): NUMBER;
begin
    Result := arctan(x);
end;
//------------------------------------------------------------------------------
function _sinh(const x: NUMBER): NUMBER;
begin
    Result := (exp(x)-exp(-x))*0.5;
end;
//------------------------------------------------------------------------------
function _cosh(const x: NUMBER): NUMBER;
begin
    Result := (exp(x)+exp(-x))*0.5;
end;
//------------------------------------------------------------------------------
function _cotan(const x: NUMBER): NUMBER;
begin
    Result := cotan(x);
end;
//------------------------------------------------------------------------------
function _tan(const x: NUMBER): NUMBER;
begin
    Result := tan(x);
end;
//------------------------------------------------------------------------------
function _exp(const x: NUMBER): NUMBER;
begin
  Result := exp(x);
end;
//------------------------------------------------------------------------------
function _ln(const x: NUMBER): NUMBER;
begin
  Result := ln(x);
end;
//------------------------------------------------------------------------------
function _log10(const x: NUMBER): NUMBER;
begin
  Result := log10(x);
end;
//------------------------------------------------------------------------------
{
function _log2(const x: NUMBER): NUMBER;
begin
    Result := log2(x);
end;
}
//------------------------------------------------------------------------------
function _logN(const x,y: NUMBER): NUMBER;
begin
    Result := logN(x, y);
end;
//------------------------------------------------------------------------------
function _sqrt(const x: NUMBER): NUMBER;
begin
    Result := sqrt(x);
end;
//------------------------------------------------------------------------------
function _abs(const x: NUMBER): NUMBER;
begin
    Result := abs(x);
end;
//------------------------------------------------------------------------------
function _min(const x,y: NUMBER): NUMBER;
begin
    if x < y then
      Result := x
    else
      Result := y;
end;
//------------------------------------------------------------------------------
function _max(const x,y: NUMBER): NUMBER;
begin
    if x > y then
      Result := x
    else
      Result := y;
end;
//------------------------------------------------------------------------------
function _sign(const x: NUMBER): NUMBER;
begin
  if x < 0 then
    Result := -1
  else
    if x > 0 then
      Result := 1.0
    else
      Result := 0.0;
end;
//------------------------------------------------------------------------------
function _trunc(const x: NUMBER): NUMBER;
begin
  Result := int(x);
end;
//------------------------------------------------------------------------------
function _ceil(const x: NUMBER): NUMBER;
begin
  if frac(x) > 0 then
    Result := int(x + 1)
  else
    Result := int(x);
end;
//------------------------------------------------------------------------------
function _floor(const x: NUMBER): NUMBER;
begin
  if frac(x) < 0 then
    Result := int(x - 1)
  else
    Result := int(x);
end;
//------------------------------------------------------------------------------
function _rnd(const x: NUMBER): NUMBER;
begin
   Result := int(Random * int(x));
end;
//------------------------------------------------------------------------------
function _random(const x: NUMBER): NUMBER;
begin
  Result := Random * x;
end;
//------------------------------------------------------------------------------
function _if(const x : array of NUMBER): NUMBER;
begin
  if x[0]<>0 then
    Result := x[1]
  else
    Result := x[2];
end;
//------------------------------------------------------------------------------

//------------------------------------------------------------------------------
////////////////////////////////////////////////////////////////////////////////
constructor TBasicNode.Create(Val : NUMBER);
begin
     inherited Create;
     Value := Val;
end;
//------------------------------------------------------------------------------
destructor TBasicNode.Destroy;
begin
     inherited Destroy;
end;
//------------------------------------------------------------------------------
function TBasicNode.GetValue;
begin
     Result := Value;
end;
//------------------------------------------------------------------------------
function TBasicNode.IsUsed(Addr: Pointer): boolean;
begin
     Result:= false; //a basic node does not store any variable or function info
end;
//------------------------------------------------------------------------------
//since basic node cannot be optimized further, this function does nothing
procedure TBasicNode.Optimize; //Optimize evaluates constant values at compile time.
begin
  //do nothing.
end;
//------------------------------------------------------------------------------
////////////////////////////////////////////////////////////////////////////////
constructor TOneParamNode.Create(ChildNode: TNode; FuncAddr : TOneParamFunc);
begin
     inherited Create;
     Child := ChildNode;
     Func  := FuncAddr;
end;
//------------------------------------------------------------------------------
destructor TOneParamNode.Destroy;
begin
     Child.Free;
     inherited Destroy;
end;
//------------------------------------------------------------------------------
function TOneParamNode.GetValue;
begin
     Result := Func(Child.GetValue);
end;
//------------------------------------------------------------------------------
function TOneParamNode.IsUsed(Addr: Pointer): boolean;
begin
     Result:= (Addr = @Func) or Child.IsUsed(Addr);
end;
//------------------------------------------------------------------------------
procedure TOneParamNode.Optimize; //Optimize evaluates constant values at compile time.
begin
  OptimizeNode(Child);
end;
//------------------------------------------------------------------------------
////////////////////////////////////////////////////////////////////////////////
constructor TTwoParamNode.Create(LeftNode, RightNode: TNode; FuncAddr : TTwoParamFunc);
begin
     inherited Create;
     Left  := LeftNode;
     Right := RightNode;
     Func  := FuncAddr;
end;
//------------------------------------------------------------------------------
destructor TTwoParamNode.Destroy;
begin
     Left.Free;
     Right.Free;
     inherited Destroy; //skipping TNode.Destroy which is abstract.
end;
//------------------------------------------------------------------------------
function TTwoParamNode.GetValue;
begin
     Result := Func(Left.GetValue, Right.GetValue);
end;
//------------------------------------------------------------------------------
function TTwoParamNode.IsUsed(Addr: Pointer): boolean;
begin
     Result:= (Addr = @Func) or Left.IsUsed(Addr) or Right.IsUsed(Addr);
end;
//------------------------------------------------------------------------------
procedure TTwoParamNode.Optimize; //Optimize evaluates constant values at compile time.
begin
  OptimizeNode(Left);
  OptimizeNode(Right);
end;
//------------------------------------------------------------------------------
////////////////////////////////////////////////////////////////////////////////
constructor TNParamNode.Create(var Nodes : TList; FuncAddr : TNParamFunc);
begin
  inherited Create;
  Children := Nodes;
  Func  := FuncAddr;
end;
//------------------------------------------------------------------------------
destructor TNParamNode.Destroy;
  var i : Integer;
begin
  for i:= Children.Count-1 downto 0 do
  begin
    TNode(Children.Items[i]).Free;
  end;
  Children.Free;
  inherited Destroy;
end;
//------------------------------------------------------------------------------
function TNParamNode.GetValue;
  var Values : PDoubleArray;
      i : Integer;
begin
  GetMem(Values, Children.Count * SizeOf(NUMBER));
  try
    for i := 0 to Children.Count-1 do
    begin
      Values^[i]:= TNode(Children.Items[i]).GetValue;
    end;
    Result := Func(Values^);
  finally
    FreeMem(Values);
  end;
end;
//------------------------------------------------------------------------------
function TNParamNode.IsUsed(Addr: Pointer): boolean;
  var i: Integer;
begin
  Result:= (Addr = @Func);
  for i := 0 to Children.Count-1 do
  begin
    if (TNode(Children.Items[i])).IsUsed(Addr) then
    begin
      Result := true;
      exit;
    end;
  end;
end;
//------------------------------------------------------------------------------
procedure TNParamNode.Optimize; //Optimize evaluates constant values at compile time.
  var i: Integer;
      aNode : TNode;
begin
  for i := 0 to Children.Count-1 do
  begin
    aNode:= TNode(Children.Items[i]);
    OptimizeNode(aNode);
    Children.Items[i] := aNode;
  end;
end;

//------------------------------------------------------------------------------
////////////////////////////////////////////////////////////////////////////////
constructor TVarNode.Create(variable: PNUMBER);
begin
     inherited Create;
     pVar  := variable;
end;
//------------------------------------------------------------------------------
destructor TVarNode.Destroy;
begin
      inherited Destroy;
end;
//------------------------------------------------------------------------------
function TVarNode.GetValue;
begin
     Result := pVar^;
end;
//------------------------------------------------------------------------------
function TVarNode.IsUsed(Addr: Pointer): boolean;
begin
     Result:= (Addr = pVar);
end;
//------------------------------------------------------------------------------
//since there is no parameter used to get the value of a variable, no further
//optimization can be made.
procedure TVarNode.Optimize; //Optimize evaluates constant values at compile time.
begin
  //do nothing
end;
//------------------------------------------------------------------------------
////////////////////////////////////////////////////////////////////////////////
constructor TUnknownVarNode.Create(Parser : TbcParser; VariableName: String);
begin
     inherited Create;
     MathParser  := Parser;
     VarName     := VariableName;
end;
//------------------------------------------------------------------------------
destructor TUnknownVarNode.Destroy;
begin
      inherited Destroy;
end;
//------------------------------------------------------------------------------
function TUnknownVarNode.GetValue;
begin
  Result := 0.0;
  MathParser.OnVariableFound(MathParser, VarName, Result);
end;
//------------------------------------------------------------------------------
function TUnknownVarNode.IsUsed(Addr: Pointer): boolean;
begin
     if (Addr=nil) and (VariableToSearchFor=VarName) then begin
       Result := true;
     end else begin
       Result:= false; //this method is not supported for TUnknownVarNode type.
     end;
end;
//------------------------------------------------------------------------------
//since there is no parameter used to get the value of a variable, no further
//optimization can be made.
procedure TUnknownVarNode.Optimize; //Optimize evaluates constant values at compile time.
begin
  //do nothing
end;
//------------------------------------------------------------------------------
////////////////////////////////////////////////////////////////////////////////
constructor TbcParser.Create(AOwner: TComponent);
begin
     inherited Create(AOwner);

     FExpression := '';
     FNode := nil;
     FDirty:= true; //means it is not parsed yet.
     FOptimizationOn := true;

     FVariables := TStringList.Create;
     FOneParamFuncs := TStringList.Create;
     FTwoParamFuncs := TStringList.Create;
     FNParamFuncs   := TStringList.CReate;

     FVariables.Sorted := true;
     FOneParamFuncs.Sorted := true;
     FTwoParamFuncs.Sorted := true;
     FNParamFuncs.Sorted := true;     

     //faster to make to it case sensitive:
     //FVariables.CaseSensitive := false;
     //FOneParamFuncs.CaseSensitive := false;
     //FTwoParamFuncs.CaseSensitive := false;
     //FNParamFuncs.CaseSensitive := false;

     CreateDefaultFuncs;
     CreateDefaultVars;
end;
//------------------------------------------------------------------------------
destructor TbcParser.Destroy;
begin
     FNode.Free;
     DeleteAllVars; //we need to dispose the pointers that hold numbers.
     DeleteAllFuncs;
     FVariables.Free;
     FOneParamFuncs.Free;
     FTwoParamFuncs.Free;
     FNParamFuncs.Free;
     inherited Destroy;
end;
//------------------------------------------------------------------------------
//Return -1, if all is OK, else return the index of the character where problem is at.
function CheckBrackets(const formula : string) : integer;
var i, n: integer;
begin
     //this function checks to see if the order and number of brackets are correct
     //it will say ok if it sees something like 3+()()
     //Result:= false;
     n:=0;
     for i:= 1 to Length(formula) do //if length<1 loop won't execute
     begin
          case formula[i] of
               '(': inc(n);
               ')': dec(n);
          end;
          if n<0 then begin
            Result := i;
            exit; //at any moment if expression is valid we cannot have more ) then (
          end;
     end;
     if n=0 then begin
          Result := -1;
     end else begin
          Result := Length(formula)+1;
     end;
end;
//------------------------------------------------------------------------------
procedure TbcParser.Parse;
var
   temp : string;
   i, j, brackets : integer;
begin
   if not (Length(FExpression) > 0) then
   begin
     FNode.Free; //free doesn't assign nil to the pointer...
     FNode := nil;
     raise EbcParserError.Create('Expression is empty.', '', '');
   end;
   //we will check for uppercase version of function defs
   temp := UpperCase(FExpression); //FExpression is a member variable of the class
   //replace space chars with empty string
   //temp := StringReplace(temp, ' ', '', [ rfReplaceAll ] ); //eat the spaces

   (*
   temp := StringReplace(temp, '[', '(', [ rfReplaceAll ] ); //convert parantheses to standard type
   temp := StringReplace(temp, ']', ')', [ rfReplaceAll ] ); //convert parantheses to standard type

   temp := StringReplace(temp, '{', '(', [ rfReplaceAll ] ); //convert parantheses to standard type
   temp := StringReplace(temp, '}', ')', [ rfReplaceAll ] ); //convert parantheses to standard type
   *)

   //faster way to replace the paranthesis:
   j:= Length(temp);
   for i := 1 to trunc(j/2)+1 do
   begin
        if (temp[i] = '[') or (temp[i] = '{') then //scanning half way from start
             temp[i] := '('
        else if (temp[i] = ']') or (temp[i] = '}') then
                 begin
                      temp[i] := ')';
                 end;

        if (temp[j] = '[') or (temp[j] = '{') then //scanning other half from end
             temp[j] := '('
        else if (temp[j] = ']') or (temp[j] = '}') then
                 begin
                      temp[j] := ')';
                 end;
        Dec(j);
   end;

   FNode.Free; //free the previous parse tree
   FNode := nil; //should assign NULL to make sure we don't free when object doesn't exist.

      brackets := CheckBrackets(temp);
			if(brackets>-1) and (brackets<Length(temp)+1) then
			begin
				raise EbcParserError.Create(Format('Bracket mismatch in expression %s at index %s.', [temp, IntToStr(brackets)]), Copy(temp, 1, brackets), temp);
			end
			else
			if(brackets=Length(temp))then
			begin
				raise EbcParserError.Create(Format('Missing bracket ")" in expression %s.', [temp]), temp, temp);
			end;

   //call the recursive parsing function to generate the node structure tree

   if not CreateParseTree(temp, FNode) then
   begin
      FNode.Free; //free doesn't assign nil to the pointer...
      FNode := nil;
      raise EbcParserError.Create('Syntax error in expression ' + FExpression, temp, temp);
   end;

   if FOptimizationOn then
        Optimize; //will make sure FNode tree is lean and mean

   FDirty := false; //note that we parsed it once. Unless the expression is changed we do not need to reparse it.
end;
//------------------------------------------------------------------------------
function TbcParser.Evaluate : NUMBER;
begin
  if (FDirty) then //if the expression has been changed, we need to parse it again
  begin
    Parse;
  end;
  Result := FNode.GetValue; //this will start the chain reaction to get the
                            //value of all nodes
end;
//------------------------------------------------------------------------------
function RemoveOuterBrackets(var formula: string): Boolean; //removes unncessary outer brackets in an expression
var temp: string;
    Len: integer;
begin
     Result := false;
     //has to be careful about (X+1)-(Y-1)
     //should not remove the outer brackets here thinking that they are unnecessary
     //but should remove when ((X+1)-(Y-1))
     temp:= formula;
     //ShowMessage(copy('hello', 2, 0));
     //Copy('hello', 2, 0) does not return empty string!!
     Len:= Length(temp);
     while (Len>2) and (temp[1] = '(') and (temp[Len] = ')') do
     begin
        temp:= copy(temp, 2, Length(temp)-2);
        temp := Trim(temp); //after copy, "(((2-x)*3) )" would become "((2-x)*3) "
        if CheckBrackets(temp)=-1 then begin//if we did not screw up then assign to the return value
            Result := true;
            formula:= temp;
        end;
        Len:= Length(temp);
     end;
end;
//------------------------------------------------------------------------------
function IsValidNumber(const formula : string; var Number : NUMBER) : boolean;
var code: integer;
begin
  if ((Length(formula)>1) and ((formula[1]='0') and not (formula[2]='.') )) then
  begin
    Result := false;
  end
  else
  begin
    try
      //If it starts with A-Z, then it is not a number.
      //For example Val(...) procedure thinks E-1 is a valid number.
      if (formula[1]>='A') and (formula[1]<='Z') then begin
        Result := false;
      end else begin
        Val(formula, Number, code);
        Result := (code = 0); //if true then it is a number, code is the position the error occured.
      end;
    except
	// With + and - operator as the whole expression empty formula[] is passed to this function and causes access violation?
        Result := false; 
    end;
  end;
end;
//------------------------------------------------------------------------------
function TbcParser.CreateParseTree(const formulaText: string; var Node: TNode) : boolean;
var num : NUMBER;
    varIndex, LastOper : integer;
    paramLeft, paramRight : string;
    params : TStringList;
    Nodes  : TList;
    leftNode, rightNode   : TNode;
    funcAddr : Pointer;
    i : Integer;
    formula : string;
    WasChanged : Boolean;
begin
     Result:= false;
     formula := Trim(formulaText);
     if not (Length(formula)>0) then begin //if length is zero, exit.
      exit;
     end;

     WasChanged := RemoveOuterBrackets(formula); //remove unnecessary brackets

     if WasChanged then begin
       formula := Trim(formula);
       if not (Length(formula)>0) then begin //if length is zero, exit.
        exit;
       end;
     end;
      
     //we should first remove brackets, and then check the formula length that might be modified
     //while removing brackets.

     //if (CheckBrackets(formula)<>-1) then begin
     //   exit; //brackets does not match then return false.
     //end;

     if IsValidNumber(formula, num) then
     begin //attach a number node in the structure
           Node := TBasicNode.Create(num); //we create a number node and attach it.
           Result:= true;
     end //if it is not a simple number
     else if FVariables.Find(formula, varIndex) then
          begin
               Node:= TVarNode.Create(PNUMBER(FVariables.Objects[varIndex])); //recursion will end on these points when we get to the basics
               Result:= true;
          end
          else if Assigned(OnVariableFound) and IsValidName(formula) then begin
               Node:= TUnknownVarNode.Create(self, formula); //recursion will end on these points when we get to the basics
               Result:= true;
          end
          else //if it is not a variable
          begin
            LastOper:= FindLastOper(formula);
            if not (LastOper>1) then //if it is 1 then it is a unary operation which is a one param function
            begin
               if IsOneParamFunc(formula, paramLeft, TOneParamFunc(funcAddr), LastOper) then
               begin
                    if not CreateParseTree(paramLeft, leftNode) then begin
                      FreeParseTree;
                      raise EbcParserError.Create(Format('Sub expression <%s> in <%s> is not valid.', [paramLeft, formula]), paramLeft, formula);
                      //exit;
                    end;
                    Node:= TOneParamNode.Create(leftNode, funcAddr);
                    Result:= true;
                    exit; //if it is a one param function then we exit, otherwise below code will execute
               end;
            end;
            if IsTwoParamFunc(formula, paramLeft, paramRight, TTwoParamFunc(funcAddr), LastOper) then
            begin
              if (not CreateParseTree(paramLeft, leftNode)) then begin
                FreeParseTree;
                raise EbcParserError.Create(Format('Sub expression <%s> in <%s> is not valid.', [paramLeft, formula]), paramLeft, formula);
              end;
              if (not CreateParseTree(paramRight, rightNode)) then begin
                leftNode.Free;
                FreeParseTree;
                raise EbcParserError.Create(Format('Sub expression <%s> in <%s> is not valid.', [paramRight, formula]), paramRight, formula);
              end;
              Node:= TTwoParamNode.Create(leftNode, rightNode, funcAddr);
              Result:= true;
              exit;
            end;
            if IsNParamFunc(formula, params, TNParamFunc(funcAddr)) then
            begin
              if (params<>nil) then
              begin
                Nodes:= TList.Create();
                for i:=0 to params.Count-1 do
                begin
                  paramLeft := params[i];
                  if not CreateParseTree(paramLeft, leftNode) then
                  begin
                    DisposeList(Nodes);
                    FreeParseTree;
                    raise EbcParserError.Create(Format('Sub expression <%s> in <%s> is not valid.', [paramLeft, formula]), paramLeft, formula);
                    //exit;
                  end;
                  Nodes.Add(leftNode);
                end;
                Node:= TNParamNode.Create(Nodes, funcAddr);
                params.Free;
                Result:= true;
                exit;
              end;
            end;
          end;
     //when code reaches here it means we did not exit as a result of false expression.
end;
//------------------------------------------------------------------------------
function TbcParser.FindLastOper(const formula: string) : integer;
var i, j, BracketLevel, Precedence, lastWasOperator, prevOperIndex : Integer;
    ch : Char;
begin
  Precedence := 13; //There are 12 operands and 13 is higher then all
  BracketLevel := 0; //shows the level of brackets we moved through
  Result := 0;
  lastWasOperator := 0;

  for i := 1 to Length(formula) do //from left to right scan...
  begin
    if lastWasOperator >2 then begin
      Result:= 0;
      exit;
    end;

    case formula[i] of
       ')' : begin
                dec(BracketLevel);//counting bracket levels
                lastWasOperator := 0;
             end;
       '(' : begin
                inc(BracketLevel);
                lastWasOperator := 0;
             end;

       '|' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 1 then //seeking for lowest precedence
                  begin
                       Precedence := 1;
                       Result := i; //record the current index.
                  end;
               inc(lastWasOperator);
             end;

       '&' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 2 then //seeking for lowest precedence
                  begin
                       Precedence := 2;
                       Result := i; //record the current index.
                  end;
               inc(lastWasOperator);
             end;

       '!' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 3 then //seeking for lowest precedence
                  begin
                       Precedence := 3;
                       Result := i; //record the current index.
                  end;
               inc(lastWasOperator);
             end;

       '=' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 4 then //seeking for lowest precedence
                  begin
                       Precedence := 4;
                       Result := i; //record the current index.
                  end;

               if(lastWasOperator>0)then
               begin
                prevOperIndex := i-lastWasOperator;
                if not ((formula[prevOperIndex]='<') or (formula[prevOperIndex]='>')) then begin
                        inc(lastWasOperator);
                end;
               end else begin
                inc(lastWasOperator);
               end;
             end;

             //need to correct the precedence of these folks:
       '>' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 5 then //seeking for lowest precedence
                  begin
                       Precedence := 5;
                       Result := i; //record the current index.
                  end;
               if(lastWasOperator>0) then
               begin
                if not (formula[i-lastWasOperator]='<') then begin
                        inc(lastWasOperator);
                end;
               end else begin
                inc(lastWasOperator);
               end;
             end;
       '<' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 5 then //seeking for lowest precedence
                  begin
                       Precedence := 5;
                       Result := i; //record the current index.
                  end;
               inc(lastWasOperator);
             end;


       '-' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if (Precedence >= 7) then //seeking for lowest precedence
                  begin
                       Precedence := 7;
                       Result := i; //record the current index.
                  end;
               inc(lastWasOperator);
             end;
       '+' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 7 then
                  begin
                       Precedence := 7;
                       Result := i;
                  end;
               inc(lastWasOperator);
             end;
       '%' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 9 then
                  begin
                       Precedence := 9;
                       Result := i;
                  end;
               inc(lastWasOperator);
             end;
       '/' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 9 then
                  begin
                       Precedence := 9;
                       Result := i;
                  end;
               inc(lastWasOperator);
             end;
       '*' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 9 then
                  begin
                       Precedence := 9;
                       Result := i;
                  end;
               inc(lastWasOperator);
             end;
       '^' : begin
               if not ((BracketLevel > 0) or (lastWasOperator > 0)) then //a main operation has to be outside the brackets
                  if Precedence >= 12 then
                  begin
                       Precedence := 12;
                       Result := i;
                  end;
               inc(lastWasOperator);
             end;
       'E' :
          if i > 1 then
          begin
            ch := formula[i-1];
            if ((ch >= '0') and (ch <= '9')) then
            begin//this E may be part of a number in scientific notation.
              j := i;
              while(j > 1) do
              begin //trace back.
                dec(j);
                ch := formula[j];
                if ((ch='.') or ((ch >= '0') and (ch <= '9'))) then
                begin //if it is not a function or variable name.
                  continue;
                end;
                if ((ch='_') or ((ch>='A') and (ch<='Z'))) then
                begin//is it a func or var name?
                  lastWasOperator := 0;
                  break; //break the while loop.
                end;
                inc(lastWasOperator); //it must be an operator or a paranthesis.
                break; //break the while loop.
              end;
              if ((j=1) and ((ch >= '0') and (ch <= '9'))) then
              begin
                inc(lastWasOperator);
              end
            end else begin
              lastWasOperator := 0;
            end;
          end else begin
            lastWasOperator := 0;
          end;
       ' ' :
         begin
           //break;
         end ;
    else begin //case else
           lastWasOperator := 0;
         end;
    end;
  end;
end;
//------------------------------------------------------------------------------
function TbcParser.IsTwoParamFunc( const formula : string;
                         var paramLeft, paramRight: string;
                         var funcAddr : TTwoParamFunc;
                         CurrChar : integer //gives the last operation index in the string
                         ) : boolean;
var i, funcIndex, BracketLevel, paramStart, Len, ValidCount : integer;
    c, nextCh : char;
    //temp_ : array [1..MAX_NAME_LEN] of char;
    temp : String; 
begin
  Len:= Length(formula);
  Result := false;
  if Len=0 then begin
     exit;
  end;

  if CurrChar>0 then //if function in question is an operand
  begin
      if(CurrChar>Len-1) then begin
        Result:= false;
        exit;
      end;
      c:= formula[CurrChar];
      //was it an operand also? we want to find <>, >=, <=
      if(c='<') then begin
        nextCh := formula[CurrChar+1]; //look ahead.
        if(nextCh='>')then begin
          funcAddr  := @_notequals;
          paramLeft := copy(formula, 1, CurrChar-1);
          paramRight:= copy(formula, CurrChar+2, Len-CurrChar-1);
        end else if (nextCh='=') then begin
          funcAddr  := @_ltequals;
          paramLeft := copy(formula, 1, CurrChar-1);
          paramRight:= copy(formula, CurrChar+2, Len-CurrChar-1);
        end else begin
          funcAddr  := @_less; //default case.
          paramLeft := copy(formula, 1, CurrChar-1);
          paramRight:= copy(formula, CurrChar+1, Len-CurrChar);
        end;

        if (not (Length(paramLeft)>0) ) then begin
          Result:= false;
          exit;
        end;
        if (not (Length(paramRight)>0) ) then begin
          Result:= false;
          exit;
        end;
        Result:= true; //all output is assigned, now we return true.
        exit;
      end else if(c='>')then begin
        nextCh := formula[CurrChar+1];
        if(nextCh='=') then begin
          funcAddr  := @_gtequals;
          paramLeft := copy(formula, 1, CurrChar-1);
          paramRight:= copy(formula, CurrChar+2, Len-CurrChar-1);
        end else begin
          funcAddr  := @_greater; //default case.
          paramLeft := copy(formula, 1, CurrChar-1);
          paramRight:= copy(formula, CurrChar+1, Len-CurrChar);
        end;
        if (not (Length(paramLeft)>0) ) then begin
          Result := false;
          exit;
        end;
        if (not (Length(paramRight)>0) ) then begin
          Result:= false;
          exit;
        end;
        Result:= true; //all output is assigned, now we return true.
        exit;
      end else begin //default case:
       Result:= true;
       paramLeft := copy(formula, 1, CurrChar-1);
       if not (Length(paramLeft)>0) then
       begin
            Result:= false;
            exit;
       end;

       paramRight:= copy(formula, CurrChar+1, Len-CurrChar);
       if not (Length(paramRight)>0) then
       begin
            Result:= false;
            exit;
       end;

       case c of
            //analytic operators:
            '+': funcAddr := @_add;
            '-': funcAddr := @_subtract;
            '*': funcAddr := @_multiply;
            '/': funcAddr := @_divide;
            '^': funcAddr := @_power;
            '%': funcAddr := @_intdiv;

            //logical operators:
            //'>': funcAddr := @_greater;
            //'<': funcAddr := @_less;
            '=': funcAddr := @_equal;
            '&': funcAddr := @_and;
            '|': funcAddr := @_or;
       end;
       exit; //all output is assigned, now we exit.
     end;
  end;
  //if we reach here, result is false
  //if main operation is not an operand but a function
  if (formula[Len] = ')') then //last character must be brackets closing function param list
  begin
     i:= 1;
     ValidCount:=0;
     while IsValidChar(i, formula[i]) do
     begin
          if not (i<MAX_NAME_LEN+1) then //leave space for NULL terminator also.
          begin
            raise EbcParserError.Create('Function name in ' + formula +
                   ' is too long. Maximum possible name length is ' +
                   IntToStr(MAX_NAME_LEN) + '.',  Copy(formula, 1, MAX_NAME_LEN), formula);
          end;
          //temp_[i]:= formula[i];
          Inc(i);
          Inc(ValidCount);
     end;
     while formula[i]=' ' do
     begin
       Inc(i);
     end;
     
     if (formula[i] = '(') and (i < Len) then
     begin
          //temp_[i] := chr(0);
          temp := copy(formula, 1, ValidCount);
          if FTwoParamFuncs.Find(temp, funcIndex) then
          begin
             paramStart:= Succ(i);
             funcAddr := TTwoParamFunc(FTwoParamFuncs.Objects[funcIndex]);
             BracketLevel:= 1;
             while not (i>Len-1) do //last character is a ')', that's why we use i>Len-1
             begin
                  Inc(i);
                  case formula[i] of
                       '(': inc(BracketLevel);
                       ')': dec(BracketLevel);
                       ',': begin
                                 if (BracketLevel = 1) and (i<Len-1) then //last character is a ')', that's why we use i>Len-1
                                 begin
                                      paramLeft:= copy(formula, paramStart, i-paramStart);
                                      paramRight:= copy(formula, i+1, Len-1-i); //last character is a ')', that's why we use Len-1-i
                                      Result:= true; //we are sure that it is a two parameter function
                                 end;
                            end;
                  end;
             end;
          end;
     end;
  end;
end;
//------------------------------------------------------------------------------
function TbcParser.IsOneParamFunc( const formula : string;
                         var param: string;
                         var funcAddr : TOneParamFunc;
                         CurrChar : integer
                         ) : boolean;
var i, funcIndex, paramStart, Len, ValidCount: integer;
        //temp_ : array [1..MAX_NAME_LEN] of char;
        temp : String; 
begin
  Len:= Length(formula);
  Result:= false;
  if Len=0 then begin
     exit;
  end;

  if CurrChar = 1 then //if function in question is an unary operand
  begin
       Result:= true;
       param:= copy(formula, 2, Len-1);
       if not (Length(param)>0) then
       begin
            Result:= false;
            exit;
       end;

       case formula[CurrChar] of
            '+': funcAddr := @_unaryadd;
            (* //following attempt won't work in this architecture
            begin
                      str := copy(formula, 2, Len-1); //copy the rest of the expression
                      RemoveOuterBrackets(str); //remove unnecessary brackets
                      Result:= IsOneParamFunc(str, param, funcAddr); //make a recursive call again, skipping the plus operator
            end;
            *)
            '-': funcAddr := @_negate;
            '!': funcAddr := @_not;
            else Result := false; //only + and - can be unary operators
       end;
       exit; //all output is assigned, now we exit.
  end;
  //if we reach here, result is false
  //if main operation is not an operand but a function
  if (formula[Len] = ')') then //last character must be brackets closing function param list
  begin
     i:= 1;
     ValidCount:= 0;
     while IsValidChar(i, formula[i]) do
     begin
          if not (i<MAX_NAME_LEN+1) then //leave space for NULL terminator also.
          begin
            raise EbcParserError.Create('Function name in ' + formula +
                   ' is too long. Maximum possible name length is ' +
                   IntToStr(MAX_NAME_LEN) + '.', Copy(formula, 1, MAX_NAME_LEN), formula);
          end;
          //temp_[i]:= formula[i];
          Inc(i);
          Inc(ValidCount);
     end;
     while formula[i]=' ' do
     begin
       Inc(i);
     end;
     if (formula[i] = '(') and (i < Len-1) then
     begin
          temp := copy(formula, 1, ValidCount);
          //temp_[i] := chr(0);
          if FOneParamFuncs.Find(temp, funcIndex) then
          begin
             paramStart:= Succ(i);
             funcAddr := TOneParamFunc(FOneParamFuncs.Objects[funcIndex]);
             param:= copy(formula, paramStart, Len-paramStart); //check example: SIN(30)
             Result:= true //we are sure that it is a two parameter function
          end;
     end;
  end;
end;
//------------------------------------------------------------------------------
function TbcParser.IsNParamFunc( const formula : string;
                         var params: TStringList;
                         var funcAddr : TNParamFunc
                         ) : boolean;
var i, funcIndex, BracketLevel, paramStart, Len, pIndex, nParams, ValidCount : integer;
    funcRecord : TNParamFuncRecord;
    //temp_ : array [1..MAX_NAME_LEN] of char;
    temp : String; 
begin
  Len:= Length(formula);
  Result := false;
  if Len=0 then begin
     exit;
  end;

  if (formula[Len] = ')') then //last character must be brackets closing function param list
  begin
     i:= 1;
     ValidCount:=0;
     while IsValidChar(i, formula[i]) do
     begin
          if not (i<MAX_NAME_LEN+1) then //leave space for NULL terminator also.
          begin
            raise EbcParserError.Create('Function name in ' + formula +
                   ' is too long. Maximum possible name length is ' +
                   IntToStr(MAX_NAME_LEN) + '.', Copy(formula, 1, MAX_NAME_LEN), formula);
          end;
          //temp_[i]:= formula[i];
          Inc(i);
          Inc(ValidCount);
     end;
     while formula[i]=' ' do
     begin
       Inc(i);
     end;
     
     if (formula[i] = '(') and (i < Len) then
     begin
          //temp_[i] := chr(0);
          temp := copy(formula, 1, ValidCount);
          if FNParamFuncs.Find(temp, funcIndex) then
          begin
             paramStart:= Succ(i);
             funcRecord := PNParamFuncRecord(FNParamFuncs.Objects[funcIndex])^;
             funcAddr := funcRecord.Func;
             nParams  := funcRecord.nParam;
             if nParams>2 then
             begin
               params := TStringList.Create; //must not forget to free outside this function.
               BracketLevel:= 1;
               pIndex := 0; //parameter index.
               while not (i>Len-1) do //last character is a ')', that's why we use i>Len-1
               begin
                  Inc(i);
                  case formula[i] of
                       '(': inc(BracketLevel);
                       ')': dec(BracketLevel);
                       ',': begin
                                 if (BracketLevel = 1) and (i<Len-1) then //last character is a ')', that's why we use i>Len-1
                                 begin
                                    if not (pIndex<nParams) then
                                    begin
                                      Result:= false; //wrong number of parameters.
                                    end else begin
                                      params.Add(copy(formula, paramStart, i-paramStart));
                                      inc(pIndex);
                                      if pIndex=nParams-1 then
                                      begin
                                        //assign the last one:
                                        params.Add(copy(formula, i+1, Len-1-i));
                                        Result:= true;
                                        exit;
                                      end;
                                      paramStart := i+1;
                                    end;
                                 end;
                            end;
                  end;
               end;
             end;
          end;
     end;
  end;
end;
//------------------------------------------------------------------------------
procedure TbcParser.CreateVar(const name: string; const value: NUMBER);
var index : integer;
    p: ^NUMBER;
begin
  if Length(name)>MAX_NAME_LEN then begin
    raise EbcParserError.Create(name + ' is too long. Maximum variable or function name length is ' + IntToStr(MAX_NAME_LEN) + '.', Copy(name, 1, MAX_NAME_LEN), name);
  end;
  if not IsValidName(AnsiUpperCase(name)) then begin
    raise EbcParserError.Create(name + ' is not a valid variable name.', name, name);
  end;

  if (FVariables.Find(name, index)) then //to use TStringList.Find, the list must be sorted.
    PNUMBER(FVariables.Objects[index])^ := value //just assign value if already there.
  else begin
           New(p);
           p^:= value;
           FVariables.AddObject(name, TOBJECT(p)); //add the variables and the object to hold the value with it.
           FDirty:= true; //previously bad expression may now be ok, we should reparse it
       end;
end;
//------------------------------------------------------------------------------
procedure TbcParser.CreateOneParamFunc(const name: string; procAddr: TOneParamFunc);
var index : integer;
begin
  if Length(name)>MAX_NAME_LEN then begin
    raise EbcParserError.Create(name + ' is too long. Maximum variable or function name length is ' + IntToStr(MAX_NAME_LEN) + '.', Copy(name,1,MAX_NAME_LEN), name);
  end;
  if not IsValidName(AnsiUpperCase(name)) then begin
    raise EbcParserError.Create(name + ' is not a valid function name.',name,name);
  end;

  if (FOneParamFuncs.Find(name, index)) or (FTwoParamFuncs.Find(name, index)) then
    raise EbcParserError.Create('Function ' + name + ' already exists.',name,name)
  else
      FOneParamFuncs.AddObject(name, TObject(@procAddr)); //add the variables and the object to hold the value with it.

  FDirty:= true; //previously bad expression may now be ok, we should reparse it
end;
//------------------------------------------------------------------------------
procedure TbcParser.CreateTwoParamFunc(const name: string; procAddr: TTwoParamFunc);
var index : integer;
begin
  if Length(name)>MAX_NAME_LEN then begin
    raise EbcParserError.Create(name + ' is too long. Maximum variable or function name length is ' + IntToStr(MAX_NAME_LEN) + '.', Copy(name,1,MAX_NAME_LEN), name);
  end;
  if not IsValidName(AnsiUpperCase(name)) then begin
    raise EbcParserError.Create(name + ' is not a valid function name.',name,name);
  end;

  if (FOneParamFuncs.Find(name, index)) or (FTwoParamFuncs.Find(name, index)) then
  begin
    raise EbcParserError.Create('Function ' + name + ' already exists.',name,name)
  end
  else
    FTwoParamFuncs.AddObject(name, TObject(@procAddr)); //add the variables and the object to hold the value with it.

  FDirty:= true; //previously bad expression may now be ok, we should reparse it
end;
//------------------------------------------------------------------------------
procedure TbcParser.CreateNParamFunc(const name: string; procAddr: TNParamFunc; nParam : Integer);
var index : integer;
    funcRecord : PNParamFuncRecord;

begin
  if Length(name)>MAX_NAME_LEN then begin
    raise EbcParserError.Create(name + ' is too long. Maximum variable or function name length is ' + IntToStr(MAX_NAME_LEN) + '.', Copy(name,1,MAX_NAME_LEN), name);
  end;
  if not IsValidName(AnsiUpperCase(name)) then begin
    raise EbcParserError.Create(name + ' is not a valid function name.',name,name);
  end;

  if FOneParamFuncs.Find(name, index)
      or FTwoParamFuncs.Find(name, index)
      or FNParamFuncs.Find(name, index) then
  begin
    raise EbcParserError.Create('Function ' + name + ' already exists.',name,name);
  end
  else begin
    New(funcRecord); //must deallocate using dispose()
    funcRecord^.Func := @procAddr;
    funcRecord^.nParam := nParam;
    FNParamFuncs.AddObject(name, TObject(funcRecord)); //add the variables and the object to hold the value with it.
  end;

  FDirty:= true; //previously bad expression may now be ok, we should reparse it
end;
//------------------------------------------------------------------------------
procedure TbcParser.AssignVar(const name: string; val: NUMBER);
var index : integer;
    //p : PNUMBER;
begin
  if (FVariables.Find(name, index)) then //to use TStringList.Find the list must be sorted.
    PNUMBER(FVariables.Objects[index])^ := val
  else
  begin //if not found, add the variable:
    CreateVar(name, val);
    {
    if Length(name)>MAX_NAME_LEN then begin
      raise EbcParserError.Create(name + ' is too long. Maximum variable or function name length is ' + IntToStr(MAX_NAME_LEN) + '.');
    end;
    if not IsValidName(AnsiUpperCase(name)) then
    begin
      raise EbcParserError.Create(name + ' is not a valid variable name.');
    end;
    New(p);
    p^:= value;
    FVariables.AddObject(name, TOBJECT(p)); //add the variables and the object to hold the value with it.
    FDirty := true;
    }
  end;
end;
//------------------------------------------------------------------------------
function TbcParser.GetVar(const name: string) : NUMBER;
var index : integer;
begin
  if (FVariables.Find(name, index)) then //to use TStringList.Find the list must be sorted.
      Result:= PNUMBER(FVariables.Objects[index])^
  else
    raise EbcParserError.Create('Variable ' + name + ' is not found.',name,name);
end;
//------------------------------------------------------------------------------
procedure TbcParser.DeleteVar(const name: string);
var index : integer;
begin
     //this function deletes the variable only if it finds it.
     if (FVariables.Find(name, index)) then //to use TStringList.Find the list must be sorted.
     begin
          Dispose(PNUMBER(FVariables.Objects[index]));
          FVariables.Delete(index);
          FDirty := true;
     end;
     //keep silent if not found:
     //else
     //   raise EbcParserError.Create('Variable ' + name + ' is not found. Cannot delete non-existent variable.');
end;
//------------------------------------------------------------------------------
procedure TbcParser.DeleteFunc(const name: string); //one param or two param, does not matter.
var index : integer;
begin
     FDirty := true;
     if (FOneParamFuncs.Find(name, index)) then
          FOneParamFuncs.Delete(index)
     else if (FTwoParamFuncs.Find(name, index)) then
             FTwoParamFuncs.Delete(index)
     else if (FNParamFuncs.Find(name, index)) then
     begin
       Dispose(PNParamFuncRecord(FNParamFuncs.Objects[index]));
       FNParamFuncs.Delete(index);
     end
     else
     begin
       //if nothing is deleted
       FDirty := false;
       //keep silent, if not found, fine...
       //raise EbcParserError.Create('Function ' + name + ' is not found. Cannot delete non-existent function.');
     end;
end;
//------------------------------------------------------------------------------
function TbcParser.GetExpression : string;
begin
     Result:= FExpression;
end;
//------------------------------------------------------------------------------
procedure TbcParser.SetExpression(const str: string);
begin
     FExpression:= str;
     FDirty := true;
end;
//------------------------------------------------------------------------------
procedure TbcParser.CreateDefaultVars;
begin
     CreateVar('PI', 3.14159265358979);
     CreateVar('X', 0.0);
     CreateVar('Y', 0.0);
end;
//------------------------------------------------------------------------------
procedure TbcParser.CreateDefaultFuncs;
begin
     CreateOneParamFunc('SQR', @_square);
     CreateOneParamFunc('SIN', @_sin);
     CreateOneParamFunc('COS', @_cos);
     CreateOneParamFunc('ATAN', @_arctan);
     CreateOneParamFunc('SINH', @_sinh);
     CreateOneParamFunc('COSH', @_cosh);
     CreateOneParamFunc('COTAN', @_cotan);
     CreateOneParamFunc('TAN', @_tan);
     CreateOneParamFunc('EXP', @_exp);
     CreateOneParamFunc('LN', @_ln);
     CreateOneParamFunc('LOG', @_log10);
     CreateOneParamFunc('SQRT', @_sqrt);
     CreateOneParamFunc('ABS', @_abs);
     CreateOneParamFunc('SIGN', @_sign);
     CreateOneParamFunc('TRUNC', @_trunc);
     CreateOneParamFunc('CEIL', @_ceil);
     CreateOneParamFunc('FLOOR', @_floor);
     CreateOneParamFunc('RND', @_rnd);
     CreateOneParamFunc('RANDOM', @_random);

     CreateTwoParamFunc('INTPOW', @_intpower);
     CreateTwoParamFunc('POW', @_power);
     CreateTwoParamFunc('LOGN', @_logn);
     CreateTwoParamFunc('MIN', @_min);
     CreateTwoParamFunc('MAX', @_max);
     CreateTwoParamFunc('MOD', @_modulo);

     CreateNParamFunc('IF', @_if, 3);
end;
//------------------------------------------------------------------------------
procedure TbcParser.DeleteAllVars;
var i: integer;
begin
   for i:= FVariables.Count-1 downto 0 do
   begin
        Dispose(PNUMBER(FVariables.Objects[i]));
        FVariables.Delete(i);
   end;
   FDirty := true;
end;
//------------------------------------------------------------------------------
procedure TbcParser.DeleteAllFuncs;
var i: integer;
begin
     for i:= FOneParamFuncs.Count-1 downto 0 do
          FOneParamFuncs.Delete(i);
     for i:= FTwoParamFuncs.Count-1 downto 0 do
          FTwoParamFuncs.Delete(i);
     for i:= FNParamFuncs.Count-1 downto 0 do
     begin
       Dispose(PNParamFuncRecord(FNParamFuncs.Objects[i]));
       FNParamFuncs.Delete(i);
     end;
     FDirty := true;
end;
//------------------------------------------------------------------------------
function TbcParser.GetFunctions: TStringList;
  var FuncList : TStringList;
begin
  FuncList := TStringList.Create;
  FuncList.AddStrings(FOneParamFuncs);
  FuncList.AddStrings(FTwoParamFuncs);
  FuncList.AddStrings(FNParamFuncs);
  Result:= FuncList;
end;
//------------------------------------------------------------------------------
function TbcParser.GetVariables: TStringList;
  var VarList : TStringList;
begin
  VarList := TStringList.Create;
  VarList.AddStrings(FVariables);
  Result:= VarList;
end;
//------------------------------------------------------------------------------
//Find TUnknownVarNode variables used in the tree of nodes.
procedure FindVariablesUsed(var aNode: TNode; var VarList : TStringList);
var
  childNode : TNode;
  twoParamNode : TTwoParamNode;
  oneParamNode : TOneParamNode;
  nParamNode : TNParamNode;
  unknownVarNode : TUnknownVarNode;
  i : integer;
begin
  if (aNode is TUnknownVarNode) then
  begin
    unknownVarNode := aNode as TUnknownVarNode;
    VarList.Add(unknownVarNode.VarName);
  end
  else if (aNode is TTwoParamNode) then
  begin
    twoParamNode := aNode as TTwoParamNode;
    FindVariablesUsed(twoParamNode.Left, VarList);
    FindVariablesUsed(twoParamNode.Right, VarList);
  end
  else
  if (aNode is TOneParamNode) then
  begin
    oneParamNode := aNode as TOneParamNode;
    FindVariablesUsed(oneParamNode.Child, VarList);
  end
  else
  if (aNode is TNParamNode) then
  begin
    nParamNode := aNode as TNParamNode;

    for i:=nParamNode.Children.Count-1 downto 0 do
    begin
      childNode := TNode(nParamNode.Children.Items[i]);
      FindVariablesUsed(childNode, VarList);
    end;
  end;

end;
//------------------------------------------------------------------------------
function TbcParser.GetVariablesUsed: TStringList;
  var VarList : TStringList;
      DefinedVarList : TStringList;
      i, Len : integer;
      Str : String;
begin
  if (FDirty) and (Length(FExpression)>0) then begin
    Parse();
  end;
  VarList := TStringList.Create;
  if FNode<>nil then begin
    DefinedVarList := GetVariables();
    try
      Len := DefinedVarList.Count-1;
      for i := 0 to Len do begin
        Str := DefinedVarList.Strings[i];
        if IsVariableUsed(Str) then begin
          VarList.Add(Str);
        end;
      end;
    finally
      DefinedVarList.Free;
    end;
    FindVariablesUsed(FNode, VarList);
  end;
  Result:= VarList;
end;
//------------------------------------------------------------------------------
procedure TbcParser.SetX(const x : NUMBER);
begin
  AssignVar('X', x);
end;
//------------------------------------------------------------------------------
function TbcParser.GetX;
//var index : integer;
begin
  Result := GetVar('X');
end;
//------------------------------------------------------------------------------
procedure TbcParser.SetY(const y : NUMBER);
begin
     AssignVar('Y', y);
end;
//------------------------------------------------------------------------------
function TbcParser.GetY;
begin
  Result:= GetVar('Y');
end;
//------------------------------------------------------------------------------
function TbcParser.IsVariable(const varName: string) : boolean;
var index : integer;
begin
     if (FVariables.Find(varName, index)) then //to use TStringList.Find the list must be sorted.
          Result:= true
     else
         Result:= false;
end;
//------------------------------------------------------------------------------
function TbcParser.IsOneParamFunction(const funcName: string): boolean;
var index: integer;
begin
     if (FOneParamFuncs.Find(funcName, index)) then //to use TStringList.Find the list must be sorted.
          Result:= true
     else
         Result:= false;
end;
//------------------------------------------------------------------------------
function TbcParser.IsTwoParamFunction(const funcName: string): boolean;
var index: integer;
begin
     if (FTwoParamFuncs.Find(funcName, index)) then //to use TStringList.Find the list must be sorted.
         Result:= true
     else
         Result:= false;
end;
//------------------------------------------------------------------------------
function TbcParser.IsNParamFunction(const funcName: string): boolean;
var index: integer;
begin
     if (FNParamFuncs.Find(funcName, index)) then //to use TStringList.Find the list must be sorted.
          Result:= true
     else
         Result:= false;
end;
//------------------------------------------------------------------------------
function TbcParser.IsFunction(const funcName: string): boolean;
begin
     if (IsOneParamFunction(funcName) or IsTwoParamFunction(funcName) or IsNParamFunction(funcName)) then //to use TStringList.Find the list must be sorted.
          Result:= true
     else
         Result:= false;
end;
//------------------------------------------------------------------------------
procedure TbcParser.FreeParseTree;
begin
  FNode.Free;
  FNode:= nil;
  FDirty:= true; //so that next time we call Evaluate, it will call the Parse method.
end;
//------------------------------------------------------------------------------
function TbcParser.IsVariableUsed(const varName: string): boolean;
var index: integer;
    varAddr: Pointer;
begin
     Parse; //to create parse tree if it is not created yet.
            //we might exit this function as a result of exception at this moment
     varAddr:= nil;
     if (FVariables.Find(varName, index)) then //to use TStringList.Find the list must be sorted.
          varAddr:= FVariables.Objects[index];
     if not (varAddr = nil) then begin
        Result := FNode.IsUsed(varAddr)
     end else if Assigned(OnVariableFound) then begin
        VariableToSearchFor := varName;
        Result := FNode.IsUsed(nil);
        VariableToSearchFor := '';
     end
     else Result := false;
end;
//------------------------------------------------------------------------------
function TbcParser.IsFunctionUsed(const funcName: string): boolean;
var index: integer;
    funcAddr: Pointer;
begin
     Parse; //to create parse tree if it is not created yet.
            //we might exit this function as a result of exception at this moment
     funcAddr:= nil;
     if (FOneParamFuncs.Find(funcName, index)) then //to use TStringList.Find the list must be sorted.
          funcAddr:= FOneParamFuncs.Objects[index];
     if not (funcAddr = nil) then
        Result := FNode.IsUsed(funcAddr)
     else Result:= false; //if it is nil then it is not used.

     if not Result then //if still not found check the two param func list
     begin
       funcAddr:= nil;
       if (FTwoParamFuncs.Find(funcName, index)) then //to use TStringList.Find the list must be sorted.
            funcAddr:= FOneParamFuncs.Objects[index];
       if not (funcAddr = nil) then
          Result := FNode.IsUsed(funcAddr)
       else Result:= false;
     end;
end;
//------------------------------------------------------------------------------
//a global function that optimizes any given tree branch
procedure OptimizeNode(var aNode: TNode);
var
  NewNode: TBasicNode;
  twoParamNode: TTwoParamNode;
  oneParamNode: TOneParamNode;
  nParamNode: TNParamNode;
  i : Integer;
begin
  aNode.Optimize; //will call Optimize for all nodes in the tree.

  if (aNode is TTwoParamNode) then
  begin
    twoParamNode := aNode as TTwoParamNode;
    if ( twoParamNode.Left is TBasicNode) and
       ( twoParamNode.Right is TBasicNode) then
    begin
      NewNode:= TBasicNode.Create( twoParamNode.GetValue );
      aNode.Free;
      aNode:= NewNode;
    end;
  end
  else
  if (aNode is TOneParamNode) then
  begin
    oneParamNode := aNode as TOneParamNode;
    if ( oneParamNode.Child is TBasicNode) then
    begin
      NewNode:= TBasicNode.Create (oneParamNode.GetValue);

      aNode.Free;
      aNode:= NewNode;
    end;
  end
  else
  if (aNode is TNParamNode) then
  begin
    nParamNode := aNode as TNParamNode;

    for i:=nParamNode.Children.Count-1 downto 0 do
    begin
      if not (TNode(nParamNode.Children.Items[i]) is TBasicNode) then
      begin
        exit; //this is not a basic node, exit.
      end;
    end;
    //if we arrive here, means all children are basic nodes:
    NewNode:= TBasicNode.Create(nParamNode.GetValue);
    aNode.Free;
    aNode:= NewNode;
  end;
end;
//------------------------------------------------------------------------------
procedure TbcParser.Optimize;
begin
  OptimizeNode(FNode);
end;
//------------------------------------------------------------------------------
procedure TbcParser.Randomize;
begin
  System.Randomize;
end;
//------------------------------------------------------------------------------

////////////////////////////////////////////////////////////////////////////////
//TbcParserEx BELOW:

//------------------------------------------------------------------------------
//This function is used to map numeric parameters to the actual strings through the
//ParserList global variable. The x parameter below is assumed to be the index to this
//ParserList. y parameter is assumed to be the index into the FStringTable List within the
//TbcParserEx parser instance.
function ToParsedStr(const x,y: NUMBER): String;
var StringIndex : Integer;
    //p : PbcParserEX;
    p : TbcParserEx;
begin
    //Windows.MessageBox(0, PChar('Address is '+IntToStr(index)), 'Object Address', MB_OK);
    //p := PbcParserEX(Floor(x));

    //CriticalSection.Enter;
    p := TbcParserEx(ParserList[Floor(x)]);
    //CriticalSection.Leave;
    StringIndex := Floor(y);
    if (StringIndex<>Round(y)) or (StringIndex<0) or (StringIndex>=p.StringTable.Count) then begin
      raise Exception.Create('Invalid string parameter. Valid string input is required. Parameter was: '+FloatToStr(x));
    end;
    //Result := p^.StringTable[StringIndex];
    Result := p.StringTable[StringIndex];
end;
//------------------------------------------------------------------------------
function _strlen(const x,y: NUMBER): NUMBER;
begin
    Result := Length(ToParsedStr(x,y));
end;
//------------------------------------------------------------------------------
//Assuming a function that takes 3 params:
//parameters 1 and 2 together identify the String.
//parameter 3 is the number that makes sense with this string.
//For example, String can be the column name on a spread sheet,
//and the 3rd parameter is the row index.
{
function _spreadsheet_func(const x: array of NUMBER): NUMBER;
var ColName : String;
    ColIndex: Integer;
    RowIndex: Integer;
begin
    ColName := ToParsedStr(x[0],x[1]);
    //Lookup my column name in the columns list:
    ColIndex:= GlobalMyColumnList.IndexOf(ColName);
    if ColIndex=-1 then begin
      raise Exception.Create('Invalid Column Name: '+ColName);
    end;
    RowIndex := Floor(x[2]); //find row index.
    if RowIndex > GlobalMaxRowNum or GlobalRowIndex <0 then begin
      raise Exception.Create('Invalid row number: '+RowIndex);
    end;
    Result := MyData[ColIndex][RowIndex];
end;
}
//------------------------------------------------------------------------------
constructor TbcParserEx.Create(AOwner: TComponent);
var i : Integer;
begin
     inherited Create(AOwner);
     FStringTable := TStringList.Create();
     FParserIndex := 0;

     //if we are the first instance, then we need to create the ParserList too.
     //this version is NOT thread safe. User should include thread safety code as needed.
     //CriticalSection.Enter;
     if not Assigned(ParserList) then begin
        ParserList := TList.Create();
     end;
     //look for an empty slot to insert ourself to:
     for i:=0 to ParserList.Count-1 do begin
        if ParserList[i]=nil then begin
           ParserList[i]:= self;
           FParserIndex := i;
        end;
     end;
     //if no empty slot was found, add this instance at the end:
     if FParserIndex=0 then begin
       FParserIndex := ParserList.Add(self);
     end;
     Inc(ParserCount);  //added to increment parser count so that the ParserList is freed
                        // in destructor - SM: 24 May 2015
     //CriticalSection.Leave;
end;
//------------------------------------------------------------------------------
destructor TbcParserEx.Destroy;
begin
     //CriticalSection.Enter;
     if Assigned(ParserList) then begin
      Dec(ParserCount); //we are going away...
      if ParserCount=0 then begin //if we were the last one, deallocate the list.
        ParserList.Free; //when all parsers are freed, free this list also.
      end else begin //if we are not the last one, just empty our slot so some other instance can use it.
        ParserList[FParserIndex] := nil; //discard the pointer that points to us, so that someone else can use this slot.
      end;
     end;
     //CriticalSection.Leave;
     FStringTable.Free;
     inherited Destroy;
end;
//------------------------------------------------------------------------------
procedure TbcParserEx.CreateDefaultFuncs;
begin
     inherited CreateDefaultFuncs;
     CreateTwoParamFunc('STRLEN', @_strlen);
end;
//------------------------------------------------------------------------------
procedure TbcParserEx.CreateDefaultVars;
var //addr : Integer;
    addr2: Extended;
begin
     inherited CreateDefaultVars;
     //addr := Integer(@self);
     //Windows.MessageBox(0, PChar('Address is '+IntToStr(addr)), 'Object Address', MB_OK);
     addr2 := FParserIndex; //0.0+addr;
     CreateVar('_PPARSER', addr2); //address of this instance to be passed to user funcs.
end;
//------------------------------------------------------------------------------
procedure TbcParserEx.Parse;
var Temp, Original, Str : String;
    i, Len : Integer;
    Found : Boolean;
begin
  StringTable.Clear; //clear old strings.
  Temp := FExpression;
  Original := Temp;
  Len := Length(Temp);
  Found := false;
  Str := EmptyStr;
  for i:=1 to Len do begin
      if Temp[i]='"' then begin
         if Found then begin
            Found := false;
            StringTable.Add(Str);
            Str := EmptyStr;
         end else begin
          Found := true;
         end;
      end else begin
          if Found then begin
             Str := Str + Temp[i];
          end;
      end;
  end;
  //if " quote characters do not match: (did not find end quote)
  if Found then begin
     raise Exception.Create('Invalid string input. Quotes do not match: "'+Str);
  end;

  //replace the string constants with function parameters:
  Len := StringTable.Count-1;
  for i := 0 to Len do begin
    Temp := StringReplace(Temp, '"'+StringTable[i]+'"', '_PPARSER,'+IntToStr(i), [ ] ); //replace the first occurance.
  end;

  FExpression := Temp;
  try
        //Windows.MessageBox(0, PChar('Expression is '+FExpression), 'Parsing', MB_OK);
        inherited Parse;
  finally
        FExpression := Original;
  end;
end;
//------------------------------------------------------------------------------
procedure TbcParserEx.SetExpression(const str: string);
begin
     inherited SetExpression(str);
     StringTable.Clear; //remove all previous strings.
end;


//------------------------------------------------------------------------------
constructor EbcParserError.Create(const Msg : String; const ErrPortion : String; const Exp : String);
begin
  inherited Create(Msg);
  FExp := Exp;
  Ferr := ErrPortion;
end;
//------------------------------------------------------------------------------
function EbcParserError.GetInvalidPortionOfExpression: String;
begin
  Result:= FErr;
end;
//------------------------------------------------------------------------------
function EbcParserError.GetSubExpression: String;
begin
  Result:= FExp;
end;

//------------------------------------------------------------------------------
procedure Register;
begin
  RegisterComponents('BestCode', [TbcParser, TbcParserEx]);
end;

initialization
     {$IFDEF EVALVERSION}
     if (FindWindow(PChar('TAppBuilder'), nil) = 0) then
     begin
          MessageBox(0, 'This trial version of TbcParser component runs only while IDE is running.', 'Evaluation version', MB_OK);
          //Can not use Application variable, it is declared in the Forms unit.
          //Application.Terminate; //will use PostQuitMessage to terminate.
          Halt(1);
     end;
     {$ENDIF}
end.

