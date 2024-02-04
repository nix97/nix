unit Unit1;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, FileUtil, TAGraph, TASeries, Forms, Controls, Graphics,
  Dialogs, StdCtrls, ComCtrls, formula;

type

  { TForm1 }

  TForm1 = class(TForm)
    ArtFormula1: TArtFormula;
    Button1: TButton;
    Clear: TButton;
    ButtonGraph: TButton;
    ButtonRun: TButton;
    analytic: TCheckBox;
    Chart1: TChart;
    GroupBox3: TGroupBox;
    Label15: TLabel;
    Label16: TLabel;
    Label17: TLabel;
    Memo2: TMemo;
    PlotNumerik: TLineSeries;
    Plot: TCheckBox;
    GroupBox2: TGroupBox;
    SumbuY: TLineSeries;
    SumbuX: TLineSeries;
    Graph: TLineSeries;
    TabSheet3: TTabSheet;
    Tmax: TEdit;
    Editsmooth: TEdit;
    Tmin: TEdit;
    EditGraph: TEdit;
    editF1: TEdit;
    editF2: TEdit;
    editx_aks: TEdit;
    edita: TEdit;
    editb: TEdit;
    editalpha: TEdit;
    editbeta: TEdit;
    editM: TEdit;
    EditAn: TEdit;
    GroupBox1: TGroupBox;
    Label1: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    Label12: TLabel;
    Label13: TLabel;
    Label14: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Label9: TLabel;
    Memo1: TMemo;
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    TabSheet2: TTabSheet;
   // procedure ButtonGraphClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure ButtonGraphClick(Sender: TObject);
    procedure ButtonRunClick(Sender: TObject);
    procedure ClearClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    { private declarations }
  public
    { public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.lfm}

{ TForm1 }

{
Function F(t,x,y:string):extended;
var
  num:integer;
  t1,x1,y1:string;
  vars : array of string;
  vals : TCalcArray;
begin
 num:=3;
 setlength(vals,num);
 setlength(vars,num);

 vars[0] := 't1';
 vars[1] := 'x1';
 vars[2] := 'y1';
 setS(vals[0],t1);
 setS(vals[1],x1);
 setS(vals[2],y1);

 //F:=strtofloat(ArtFormula1.ComputeStr(editF1.Text,num,@vars,@vals));
end;
}

procedure TForm1.ButtonRunClick(Sender: TObject);
var
  a,b,alpha,beta,H,Tj,Xj,Yj,K1,K2,K3,K4,R1,R2,R3,R4:extended;
  M,j,i:integer;
  Ta,Xa,Ya,Ta2,Xa2,Ya2,X3,Ta3,An,Rel:array[0..10000] of extended;

   num:integer;
   t2,x2,y2,t3,tr2,xr2,yr2,tr3,xr3,yr3,tr4,xr4,yr4:string;
   vars : array of string;
   vals,vals2,vals3,vals4,vals5 : TCalcArray;

begin
 num:=3;
 setlength(vars,num);
 setlength(vals,num);
 setlength(vals2,num);
 setlength(vals3,num);
 setlength(vals4,num);

 a:=strtofloat(edita.Text);
 b:=strtofloat(editb.Text);
 alpha:=strtofloat(editalpha.Text);
 beta:=strtofloat(editbeta.Text);
 M:=strtoint(editM.Text);

 vars[0] := 't';
 vars[1] := 'x';
 vars[2] := 'y';


 H:=(b-a)/M;
 Ta[0]:=a;
 Xa[0]:=alpha;
 Ya[0]:=0;

//F1
 for j:=0 to M-1 do
 begin
 T2:=floattostr(Ta[j]);
 X2:=floattostr(Xa[j]);
 Y2:=floattostr(Ya[j]);

 setS(vals[0],T2);
 setS(vals[1],X2);
 setS(vals[2],Y2);


  Tj:=Ta[j];
  Xj:=Xa[j];
  Yj:=Ya[j];

  K1:=H*Yj;
  R1:=H*strtofloat(ArtFormula1.ComputeStr(editF1.Text,num,@vars,@vals));

  //R1:=H*F(Tj,Xj,Yj);
  K2:=H*(Yj+R1/2);

 TR2:=floattostr(Ta[j]+H/2);
 XR2:=floattostr(Xa[j]+K1/2);
 YR2:=floattostr(Ya[j]+R1/2);

 setS(vals2[0],TR2);
 setS(vals2[1],XR2);
 setS(vals2[2],YR2);

  R2:=H*strtofloat(ArtFormula1.ComputeStr(editF1.Text,num,@vars,@vals2));

  //R2:=H*F(Tj+H/2,Xj+K1/2,Yj+R1/2);

  K3:=H*(Yj+R2/2);
 TR3:=floattostr(Ta[j]+H/2);
 XR3:=floattostr(Xa[j]+K2/2);
 YR3:=floattostr(Ya[j]+R2/2);

 setS(vals3[0],TR3);
 setS(vals3[1],XR3);
 setS(vals3[2],YR3);

  R3:=H*strtofloat(ArtFormula1.ComputeStr(editF1.Text,num,@vars,@vals3));


  //R3:=H*F(Tj+H/2,Xj+K2/2,Yj+R2/2);

  K4:=H*(Yj+R3);

  TR4:=floattostr(Ta[j]+H);
  XR4:=floattostr(Xa[j]+K3);
  YR4:=floattostr(Ya[j]+R3);

  setS(vals4[0],TR4);
  setS(vals4[1],XR4);
  setS(vals4[2],YR4);

   R4:=H*strtofloat(ArtFormula1.ComputeStr(editF1.Text,num,@vars,@vals4));

  //R4:=H*F(Tj+H,Xj+K3,Yj+R3);

  Xa[j+1]:=Xj+(K1+2*K2+2*K3+K4)/6;
  Ya[j+1]:=Yj+(R1+2*R2+2*R3+R4)/6;
  Ta[j+1]:=A+H*(j+1);

 end;


 //F2

 Ta2[0]:=a;
 Xa2[0]:=0;
 Ya2[0]:=1;

 for j:=0 to M-1 do
 begin
 T2:=floattostr(Ta2[j]);
 X2:=floattostr(Xa2[j]);
 Y2:=floattostr(Ya2[j]);

 setS(vals[0],T2);
 setS(vals[1],X2);
 setS(vals[2],Y2);


  Tj:=Ta2[j];
  Xj:=Xa2[j];
  Yj:=Ya2[j];

  K1:=H*Yj;
  R1:=H*strtofloat(ArtFormula1.ComputeStr(editF2.Text,num,@vars,@vals));

  //R1:=H*F(Tj,Xj,Yj);
  K2:=H*(Yj+R1/2);

 TR2:=floattostr(Ta2[j]+H/2);
 XR2:=floattostr(Xa2[j]+K1/2);
 YR2:=floattostr(Ya2[j]+R1/2);

 setS(vals2[0],TR2);
 setS(vals2[1],XR2);
 setS(vals2[2],YR2);

  R2:=H*strtofloat(ArtFormula1.ComputeStr(editF2.Text,num,@vars,@vals2));

  //R2:=H*F(Tj+H/2,Xj+K1/2,Yj+R1/2);

  K3:=H*(Yj+R2/2);
 TR3:=floattostr(Ta2[j]+H/2);
 XR3:=floattostr(Xa2[j]+K2/2);
 YR3:=floattostr(Ya2[j]+R2/2);

 setS(vals3[0],TR3);
 setS(vals3[1],XR3);
 setS(vals3[2],YR3);

  R3:=H*strtofloat(ArtFormula1.ComputeStr(editF2.Text,num,@vars,@vals3));


  //R3:=H*F(Tj+H/2,Xj+K2/2,Yj+R2/2);

  K4:=H*(Yj+R3);

  TR4:=floattostr(Ta2[j]+H);
  XR4:=floattostr(Xa2[j]+K3);
  YR4:=floattostr(Ya2[j]+R3);

  setS(vals4[0],TR4);
  setS(vals4[1],XR4);
  setS(vals4[2],YR4);

   R4:=H*strtofloat(ArtFormula1.ComputeStr(editF2.Text,num,@vars,@vals4));

  //R4:=H*F(Tj+H,Xj+K3,Yj+R3);

  Xa2[j+1]:=Xj+(K1+2*K2+2*K3+K4)/6;
  Ya2[j+1]:=Yj+(R1+2*R2+2*R3+R4)/6;
  Ta2[j+1]:=A+H*(j+1);
 end;

 xa[0]:=alpha;
 for j:=0 to M Do
 begin
  //X3[j]:=Xa[j]+(beta-Xa[M])*Xa2[j]/Xa2[M];
  X3[j]:=Xa[j]+(beta-Xa[M])*Xa2[j]/Xa2[M];

 end;

 if analytic.Checked=true then
 begin
   setlength(vars,1);
   setlength(vals5,1);
   vars[0] := 't';

   H:=(b-a)/M;
   Ta3[0]:=a;

   for j:=0 to M do
   begin
    T3:=floattostr(Ta3[j]);
    setS(vals5[0],T3);

    An[j]:=strtofloat(ArtFormula1.ComputeStr(editAn.Text,1,@vars,@vals5));
    Ta3[j+1]:=A+H*(j+1);

   end;

   for j:=0 to M do
   begin
    Rel[j]:=abs((An[j]-X3[j])/An[j]*100);
   end;

 memo1.lines.clear;
 memo1.Lines.add('i                    T                              X Numeric                                       X Exact                                  Relative error(%)');
 memo1.Lines.add('');
 for i:=0 to M do
 begin
  memo1.Lines.Add(inttostr(i)+format(' %20.8f',[Ta[i]])+format(' %34.18e',[X3[i]])+format(' %34.18e',[An[i]])+format(' %34.18e',[Rel[i]]));

 end;

end
else

begin

 memo1.lines.clear;
 memo1.Lines.add('i                    T                                X Numeric');
 memo1.Lines.add('');
 for i:=0 to M do
 begin
  memo1.Lines.Add(inttostr(i)+format(' %20.8f',[Ta[i]])+format(' %34.18e',[X3[i]]));
 end;

end;

 if plot.Checked=true then
 begin
  for i:=0 to M do
  begin
   PlotNumerik.AddXY(Ta[i], X3[i]);
  end;


 end;
end;

procedure TForm1.ClearClick(Sender: TObject);
begin
  Graph.Clear;
  PlotNumerik.Clear;
end;

procedure TForm1.ButtonGraphClick(Sender: TObject);
var
  i,num,min,max,n,smooth: Integer;
  x2,y2:double;
  s,y,x3:string;
  vars : array of string;
  vals: TCalcArray;

begin
  try
  //Graph.Clear;

  s:=EditGraph.text;

  min:=strtoint(Tmin.text);
  max:=strtoint(Tmax.text);
  smooth:=strtoint(Editsmooth.Text);

  n:=smooth;

  num:=n;
  setlength(vals,num);
  setlength(vars,num);

  for i:=0 to N-1 do
  begin
    x2 := MIN + (MAX - MIN) * i /(N - 1);
    x3:=floattostr(x2);

    vars[i] := 't';
    setS(vals[0],x3);

    y:=ArtFormula1.ComputeStr(s,1,@vars,@vals);
    y2:=strtofloat(y);

    Graph.AddXY(x2, y2);
  end;
  except
     showmessage('Inputan salah !!!');
    end;
end;

procedure TForm1.Button1Click(Sender: TObject);
const
 sAbout = '|======== Linear Shooting Method ========|' + #13#10 +
          '' + #13#10 +
          'Version 1.0 - build 26 july 2017'+ #13#10 +
          'Created by Lukas Setiawan.' + #13#10 +
          'Copyright (c) 2017. All Rights Reserved.' + #13#10 +
          '' + #13#10 +
          'Visit www.metodenumeriku.blogspot.com' + #13#10 +
          'FB search: Metode Numerik-Plus Programnya' + #13#10 +
          'lukassetiawan@yahoo.com' + #13#10 +
          'My other creation :'+ #13#10 +
          'https://bitbucket.org/nixz97/nix/downloads/' + #13#10 +

          '' + #13#10 +
          'Educational backgroud :' + #13#10 +
          '1. SD IPPOR Eretan Wetan_Kandanghaur-Indramayu' + #13#10 +
          '2. SMP Negeri Kandanghaur' + #13#10 +
          '3. SMA BOPKRI I (BOSA) Yogyakarta' + #13#10 +
          '4. Teknik Kimia Angkatan 94 (NRP 6294015) Universitas Parahyangan Bandung' + #13#10 +
          '5. STMIK Indonesia Mandiri Bandung (NIM 36026084)' + #13#10 +
          '' + #13#10 +
          'Accepting donations for software development.'+ #13#10
         ;

begin
 MessageDlg( sAbout, mtInformation, [mbOK], 0);
end;

procedure TForm1.FormCreate(Sender: TObject);
begin

  SumbuX.AddXY(-100,0); //sumbu x
  SumbuX.AddXY(100,0); //sumbu x

  SumbuY.AddXY(0,-100); //sumbu y
  SumbuY.AddXY(0,100); //sumbu y

end;

end.

