unit unAreaPolygon;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, FileUtil, TAGraph, TASeries, Forms, Controls, Graphics,
  Dialogs, StdCtrls, Grids;

type

  { TForm1 }

  TForm1 = class(TForm)
    ButInput: TButton;
    ButRun: TButton;
    ButExample: TButton;
    ButClear: TButton;
    Button1: TButton;
    Button2: TButton;
    Chart1: TChart;
    LabelArea: TLabel;
    Label3: TLabel;
    Memo1: TMemo;
    Series1: TLineSeries;
    EditNumSide: TEdit;
    Label1: TLabel;
    sg: TStringGrid;
    procedure ButClearClick(Sender: TObject);
    procedure ButExampleClick(Sender: TObject);
    procedure ButInputClick(Sender: TObject);
    procedure ButRunClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private

  public

  end;

var
  Form1: TForm1;

implementation

uses unNote;

{$R *.lfm}

{ TForm1 }
Procedure TriangleArea(var A:extended;var B:extended;var C:extended;var Area:extended);
var
 S:extended  ;
begin
  S:=(A+B+C)/2;
  Area:=sqrt(S*(S-A)*(S-B)*(S-C));
end;

procedure TForm1.ButInputClick(Sender: TObject);//Active Grid
label akhir;
var
  i,n:integer;
begin
  n:=strtoint(EditNumSide.Text);
  if(n<3) then
  begin
    showmessage('Minimum sides/points is 3 !!!');
    goto akhir;
  end;

  if(n>100) then
  begin
    showmessage('Maximum sides/points is 100 !!!');
    goto akhir;
  end;

  sg.Cells[0,0]:='Coordinates (Xi,Yi)';
  sg.Cells[1,0]:='               Xi';
  sg.Cells[2,0]:='               Yi';


  for i:=1 to n do
  sg.cells[0,i]:='Point '+inttostr(i);
  akhir:
end;

procedure TForm1.ButExampleClick(Sender: TObject);
var
  i,n:integer;
begin
  EditNumSide.Text:='5';
  n:=strtoint(EditNumSide.Text);
  sg.Cells[0,0]:='Coordinates (Xi,Yi)';
  sg.Cells[1,0]:='               Xi';
  sg.Cells[2,0]:='               Yi';

  for i:=1 to n do
   sg.cells[0,i]:='Point '+inttostr(i);

  sg.Cells[1,1]:='1';
  sg.Cells[2,1]:='1.3';
  sg.Cells[1,2]:='3.2';
  sg.Cells[2,2]:='5.1';
  sg.Cells[1,3]:='7.6';
  sg.Cells[2,3]:='8';
  sg.Cells[1,4]:='6.5';
  sg.Cells[2,4]:='3.5';
  sg.Cells[1,5]:='4';
  sg.Cells[2,5]:='2';


end;

procedure TForm1.ButClearClick(Sender: TObject);
var
   i:integer;
begin
  Series1.Clear;

  for i := 0 to sg.rowCount-1 do
   sg.rows[i].Clear;

  EditNumSide.Clear;

  labelArea.Caption:='Area = ?';

end;

procedure TForm1.ButRunClick(Sender: TObject);
label
  akhir;
var
  i,j,k,n:integer;
  pjA,pjB,pjC,TotalLuas:extended;
  X,Y,Luas:array[1..100] of extended;
begin

  n:=strtoint(EditNumSide.Text);
  if(n<3) then
  begin
    showmessage('Minimum sides/points is 3 !!!');
    goto akhir;
  end;

  if(n>100) then
  begin
    showmessage('Maximum sides/points is 100 !!!');
    goto akhir;
  end;

  Series1.clear;
  for i:=1 to n do
  begin
    X[i]:=strtofloat(sg.Cells[1,i]);
    Y[i]:=strtofloat(sg.Cells[2,i]);
    Series1.AddXY(X[i],Y[i]);
  end;
  Series1.AddXY(X[1],Y[1]);
  {
  for i:=1 to n do
  begin
    X[i]:=strtofloat(sg.Cells[1,i]);
    Y[i]:=strtofloat(sg.Cells[2,i]);
    pjA:=sqrt(sqr(X[1]-X[2])+sqr(Y[1]-Y[2]));
    pjB:=sqrt(sqr(X[2]-X[3])+sqr(Y[2]-Y[3]));
    pjC:=sqrt(sqr(X[3]-X[1])+sqr(Y[3]-Y[1]));
  end;
   }


   j:=1;
   Luas[1]:=0;

  for k:=1 to n-2 do
  begin
  begin
    i:=1;
    pjA:=sqrt(sqr(X[i]-X[j+1])+sqr(Y[i]-Y[j+1]));
    pjB:=sqrt(sqr(X[j+1]-X[j+2])+sqr(Y[j+1]-Y[j+2]));
    pjC:=sqrt(sqr(X[j+2]-X[i])+sqr(Y[j+2]-Y[i]));
    inc(j);
    TriangleArea(pjA,pjB,pjC,Luas[k]);
   end;

  TotalLuas:=0;

  for i:=1 to n-2 do
   TotalLuas:=TotalLuas+Luas[i];

  end;

 labelArea.caption:='Area = '+floattostr(Totalluas);
 akhir:
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  form2.show;
end;

procedure TForm1.Button2Click(Sender: TObject);
const
sAbout = '|======== Area of Irregular Polygon ========|' + #13#10 +
         '' + #13#10 +
         'Version 1.0 - Build june 18, 2018'+ #13#10 +
         'Created by Lukas Setiawan' + #13#10 +
         'Copyright (c) 2018. All Rights Reserved.' + #13#10 +
         'lukassetiawan@yahoo.com' + #13#10 +
         'my other works :' + #13#10 +
         'https://bitbucket.org/nixz97/nix/downloads/' + #13#10 +

         '' + #13#10 +
         'Educational background : ' + #13#10 +
         ''+#13#10 +
         '1. SD IPPOR Eretan Wetan-Kandanghaur-Indramayu' + #13#10 +
         '2. SMP Negeri Kandanghaur' + #13#10 +
         '3. SMA BOPKRI I (BOSA) Yogyakarta' + #13#10 +
         '4. Teknik Kimia Angkatan 94 (NRP 6294015) Universitas Parahyangan Bandung' + #13#10 +
         '5. Teknik Informatika STMIK Indonesia Mandiri Bandung (NIM 36026084)' + #13#10 +
         '' + #13#10 +
         'Accepting donations for software development.'+ #13#10;

begin
MessageDlg( sAbout, mtInformation, [mbOK], 0);
end;

procedure TForm1.FormCreate(Sender: TObject);
var
  i:integer;
begin
  for i:=0 to 2 do
   sg.ColWidths[i]:=110;

end;

end.

