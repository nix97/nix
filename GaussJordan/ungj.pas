unit unGJ;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, FileUtil, Forms, Controls, Graphics, Dialogs, StdCtrls,
  Grids;

type

  { TForm1 }

  TForm1 = class(TForm)
    ButActive: TButton;
    ButExample: TButton;
    ButRun: TButton;
    ButClear: TButton;
    ButAbout: TButton;
    EdOrder: TEdit;
    Label1: TLabel;
    Grid: TStringGrid;
    Label2: TLabel;
    Memo1: TMemo;
    procedure ButAboutClick(Sender: TObject);
    procedure ButActiveClick(Sender: TObject);
    procedure ButClearClick(Sender: TObject);
    procedure ButExampleClick(Sender: TObject);
    procedure ButRunClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private

  public

  end;

var
  Form1: TForm1;

implementation

{$R *.lfm}

{ TForm1 }

procedure TForm1.FormCreate(Sender: TObject);
begin

end;

procedure TForm1.ButExampleClick(Sender: TObject);
var
  i,n:integer;
begin
  EdOrder.Text:='3';
  n:=strtoint(EdOrder.Text);

  for i:=1 to n do
  begin
   Grid.Cells[i,0]:='A'+inttostr(i)+'X'+inttostr(i);
   Grid.Cells[0,i]:='Equation-'+inttostr(i);
  end;
  Grid.Cells[n+1,0]:='Bi';


  Grid.Cells[1,1]:='2';
  Grid.Cells[2,1]:='-3';
  Grid.Cells[3,1]:='1';
  Grid.Cells[4,1]:='11';


  Grid.Cells[1,2]:='1';
  Grid.Cells[2,2]:='2';
  Grid.Cells[3,2]:='-2';
  Grid.Cells[4,2]:='-9';

  Grid.Cells[1,3]:='3';
  Grid.Cells[2,3]:='2';
  Grid.Cells[3,3]:='-3';
  Grid.Cells[4,3]:='-10';



end;

procedure TForm1.ButRunClick(Sender: TObject);
var
  i,j,k,n:integer;
  A:array[1..100,1..100] of extended;
  X:array[1..100] of extended;

  Ratio:extended;
begin
  try

  n:=strtoint(EdOrder.Text);
  for i:=1 to n do
  begin
   Grid.Cells[i,0]:='A'+inttostr(i)+'X'+inttostr(i);
   Grid.Cells[0,i]:='Equation-'+inttostr(i);
  end;
  Grid.Cells[n+1,0]:='Bi';

  for i:=1 to n do
  begin
   for j:=1 to n+1 do
   begin
    A[i,j]:=strtofloat(Grid.Cells[j,i]);
   end;
  end;



  for i:= 1 to n do
  begin
   If A[i,i]=0 then
     begin
      Showmessage('Can not zero value in diagonal !!!');
      break;
     End;

  for j:= 1 to n do
  begin
   If (i <> j) then
     begin
      Ratio:= A[j,i]/A[i,i];
      for k:= 1 to n+1 do
       A[j,k]:= A[j,k]-Ratio*A[i,k]
     end;
  end;

end;

 X[1]:=0;

 Grid.Cells[0,n+2]:='Result :';

 for i:=1 to n do
 begin
  X[i]:= A[i,n+1]/A[i,i];
  Grid.Cells[0,n+2+i]:='X'+inttostr(i)+' = ';
  Grid.Cells[1,n+2+i]:=floattostr(X[i]);

 end;

  except
    showmessage('Something wrong with input !!!');
  end;
end;


procedure TForm1.ButActiveClick(Sender: TObject);
label akhir;
var
  i,n:integer;
begin
  try
  n:=strtoint(EdOrder.Text);
  if (n>100) then
  begin
   Showmessage('Maximum n is 100 !!!');
   goto akhir;
  end;

  for i:=1 to n do
  begin
   Grid.Cells[i,0]:='A'+inttostr(i)+'X'+inttostr(i);
   Grid.Cells[0,i]:='Equation-'+inttostr(i);
  end;
  Grid.Cells[n+1,0]:='Bi';

   Grid.ColWidths[0]:=80;
   akhir:
    except
    showmessage('Something wrong with input !!!');
  end;

end;

procedure TForm1.ButAboutClick(Sender: TObject);
const
 sAbout = '|======== Systems of Linear Equations Solver ========|' + #13#10 +
          '' + #13#10 +
          'Version 1.0 - build mar 1, 2022'+ #13#10 +
          'Created by Lukas Setiawan.' + #13#10 +
          'Copyright (c) 2022. All Rights Reserved.' + #13#10 +
          '' + #13#10 +
          'Visit www.metodenumeriku.blogspot.com' + #13#10 +
          'FB search: Metode Numerik-Plus Programnya' + #13#10 +
          'lukassetiawan@yahoo.com' + #13#10 +#13#10 +
          'My other works :'+ #13#10 +
          'https://bitbucket.org/nixz97/nix/downloads/' + #13#10 +#13#10 +

          'Accepting donations for software development.'+ #13#10
         ;

begin
 MessageDlg( sAbout, mtInformation, [mbOK], 0);
end;

procedure TForm1.ButClearClick(Sender: TObject);
var
  i,j:integer;
begin
  EdOrder.Clear;
  for i:=0 to 100 do
   for j:=0 to 209 do
     Grid.Cells[i,j]:='';
end;

end.

