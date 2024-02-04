unit Unit1;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, FileUtil, OpenGLContext, TAGraph, TASeries,
  TATransformations, Forms, Controls, Graphics, Dialogs, StdCtrls, ExtCtrls,
  Menus, Grids, GL, GLU;

type

  { TForm1 }

  TForm1 = class(TForm)
    ButtonClear: TButton;
    ButtonSample: TButton;
    ButtonAktif: TButton;
    ButtonRun: TButton;
    Chart1: TChart;
    CheckBoxScale: TCheckBox;
    CheckBoxRotate: TCheckBox;
    ComboBoxRot: TComboBox;
    EditScale: TEdit;
    EditDegree: TEdit;
    GroupBox2: TGroupBox;
    GroupBox3: TGroupBox;
    Label4: TLabel;
    Label5: TLabel;
    MainMenu1: TMainMenu;
    MenuItem1: TMenuItem;
    Series2: TLineSeries;
    CheckBoxTrans: TCheckBox;
    EditTransA: TEdit;
    EditTransB: TEdit;
    EditJumSisi: TEdit;
    GroupBox1: TGroupBox;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    SeriesSumbuY: TLineSeries;
    SeriesSumbuX: TLineSeries;
    Series1: TLineSeries;
    StringGrid1: TStringGrid;
    procedure ButtonClearClick(Sender: TObject);
    procedure ButtonSampleClick(Sender: TObject);
    procedure ButtonAktifClick(Sender: TObject);
    procedure ButtonRunClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure MenuItem1Click(Sender: TObject);
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


procedure TForm1.ButtonAktifClick(Sender: TObject);
var
  i,n:integer;

begin
  StringGrid1.Cells[1,0]:='Xi';
  StringGrid1.Cells[2,0]:='Yi';

  n:=strtoint(EditJumSisi.text);

  for i:=1 to n do
  begin
   StringGrid1.Cells[0,i]:='Point-'+inttostr(i);
  end;


end;

procedure TForm1.ButtonSampleClick(Sender: TObject);
var
   i,n:integer;
begin
  StringGrid1.Cells[1,0]:='         Xi';
  StringGrid1.Cells[2,0]:='         Yi';

  EditJumSisi.text:='22';

  n:=strtoint(EditJumSisi.text);

  for i:=1 to n do
  begin
   StringGrid1.Cells[0,i]:='Point-'+inttostr(i);
  end;

  StringGrid1.Cells[1,1]:=inttostr(20);
  StringGrid1.Cells[2,1]:=inttostr(60);
  StringGrid1.Cells[1,2]:=inttostr(40);
  StringGrid1.Cells[2,2]:=inttostr(60);
  StringGrid1.Cells[1,3]:=inttostr(40);
  StringGrid1.Cells[2,3]:=inttostr(50);
  StringGrid1.Cells[1,4]:=inttostr(50);
  StringGrid1.Cells[2,4]:=inttostr(50);
  StringGrid1.Cells[1,5]:=inttostr(50);
  StringGrid1.Cells[2,5]:=inttostr(60);
  StringGrid1.Cells[1,6]:=inttostr(60);
  StringGrid1.Cells[2,6]:=inttostr(60);
  StringGrid1.Cells[1,7]:=inttostr(60);
  StringGrid1.Cells[2,7]:=inttostr(40);
  StringGrid1.Cells[1,8]:=inttostr(50);
  StringGrid1.Cells[2,8]:=inttostr(40);
  StringGrid1.Cells[1,9]:=inttostr(50);
  StringGrid1.Cells[2,9]:=inttostr(30);
  StringGrid1.Cells[1,10]:=inttostr(60);
  StringGrid1.Cells[2,10]:=inttostr(30);
  StringGrid1.Cells[1,11]:=inttostr(60);
  StringGrid1.Cells[2,11]:=inttostr(20);
  StringGrid1.Cells[1,12]:=inttostr(40);
  StringGrid1.Cells[2,12]:=inttostr(20);
  StringGrid1.Cells[1,13]:=inttostr(40);
  StringGrid1.Cells[2,13]:=inttostr(30);
  StringGrid1.Cells[1,14]:=inttostr(30);
  StringGrid1.Cells[2,14]:=inttostr(30);
  StringGrid1.Cells[1,15]:=inttostr(30);
  StringGrid1.Cells[2,15]:=inttostr(30);
  StringGrid1.Cells[1,16]:=inttostr(30);
  StringGrid1.Cells[2,16]:=inttostr(20);
  StringGrid1.Cells[1,17]:=inttostr(20);
  StringGrid1.Cells[2,17]:=inttostr(20);
  StringGrid1.Cells[1,18]:=inttostr(20);
  StringGrid1.Cells[2,18]:=inttostr(40);
  StringGrid1.Cells[1,19]:=inttostr(30);
  StringGrid1.Cells[2,19]:=inttostr(40);
  StringGrid1.Cells[1,20]:=inttostr(30);
  StringGrid1.Cells[2,20]:=inttostr(50);
  StringGrid1.Cells[1,21]:=inttostr(20);
  StringGrid1.Cells[2,21]:=inttostr(50);
  StringGrid1.Cells[1,22]:=inttostr(20);
  StringGrid1.Cells[2,22]:=inttostr(60);


  CheckBoxRotate.Checked:=true;
  ComboBoxRot.ItemIndex:=1;
  editDegree.text:='135';

end;

procedure TForm1.ButtonClearClick(Sender: TObject);
var
   i:integer;
begin
  Series1.Clear;
  Series2.Clear;

  for i := 0 to StringGrid1.rowCount-1 do
   StringGrid1.rows[i].Clear;

  EditJumSisi.Clear;
  EditTransA.Clear;
  EditTransB.Clear;
  EditDegree.Clear;
  EditScale.clear;
  CheckBoxTrans.Checked:=false;
  CheckBoxRotate.Checked:=false;
  CheckBoxScale.Checked:=false;


end;

procedure TForm1.ButtonRunClick(Sender: TObject);
var
  i,n:integer;
  X,Y,X_aksen,Y_aksen:array[1..100] of real;
  a,b:real;

begin
  Series1.Clear;
  Series2.Clear;

  n:=strtoint(EditJumSisi.text);

  for i:=1 to n do
  begin
   X[i]:=strtofloat(StringGrid1.Cells[1,i]);
   Y[i]:=strtofloat(StringGrid1.Cells[2,i]);
  end;

  StringGrid1.Cells[0,0]:='Origin(blue)';

  for i:=1 to n do
  begin
   Series1.AddXY(X[i],Y[i]);  //asal (sebelum transformasi)
  end;
  Series1.AddXY(X[1],Y[1]);

  if CheckBoxTrans.Checked=true then
  begin
   CheckBoxRotate.Checked:=false;
   CheckBoxScale.Checked:=false;
   a:=strtofloat(EditTransA.Text);
   b:=strtofloat(EditTransB.Text);

    for i:=1 to n do
    begin
     X_aksen[i]:=X[i]+a;  //X_aksen=hasil pergeseran
     Y_aksen[i]:=Y[i]+b;
    end;

  end;

  if CheckBoxRotate.Checked=true then
  begin
   CheckBoxTrans.Checked:=false;
   CheckBoxScale.Checked:=false;

   if ComboBoxRot.ItemIndex=0 then
   begin
    for i:=1 to n do
    begin
     X_aksen[i]:=(cos(-strtofloat(EditDegree.text)/180*pi)*X[i])+(-sin(-strtofloat(EditDegree.text)/180*pi)*Y[i]);
     Y_aksen[i]:=(sin(-strtofloat(EditDegree.text)/180*pi)*X[i])+(cos(-strtofloat(EditDegree.text)/180*pi)*Y[i]);
    end;
   end
   else
   begin
    for i:=1 to n do
    begin
     X_aksen[i]:=(cos(strtofloat(EditDegree.text)/180*pi)*X[i])+(-sin(strtofloat(EditDegree.text)/180*pi)*Y[i]);
     Y_aksen[i]:=(sin(strtofloat(EditDegree.text)/180*pi)*X[i])+(cos(strtofloat(EditDegree.text)/180*pi)*Y[i]);
    end;
   end;

   for i:=1 to n do
   begin
    StringGrid1.Cells[1,n+2+i]:=floattostr(X_aksen[i]);
    StringGrid1.Cells[2,n+2+i]:=floattostr(Y_aksen[i]);
    StringGrid1.Cells[0,i+n+2]:='Point-'+inttostr(i);
   end;
  end;

  if CheckBoxScale.Checked=true then
  begin
   CheckBoxRotate.Checked:=false;
   CheckBoxTrans.Checked:=false;

    for i:=1 to n do
    begin
     X_aksen[i]:=strtofloat(EditScale.text)*X[i];
     Y_aksen[i]:=strtofloat(EditScale.text)*Y[i];
     end;

   for i:=1 to n do
   begin
    StringGrid1.Cells[1,n+2+i]:=floattostr(X_aksen[i]);
    StringGrid1.Cells[2,n+2+i]:=floattostr(Y_aksen[i]);
    StringGrid1.Cells[0,i+n+2]:='Point-'+inttostr(i);
   end;
  end;

  for i:=1 to n do
   begin
    StringGrid1.Cells[1,n+2+i]:=floattostr(X_aksen[i]);
    StringGrid1.Cells[2,n+2+i]:=floattostr(Y_aksen[i]);
    StringGrid1.Cells[0,i+n+2]:='Point-'+inttostr(i);
   end;

  StringGrid1.Cells[0,n+2]:='Result(red)';
  for i:=1 to n do
  begin
   Series2.AddXY(X_aksen[i],Y_aksen[i]);  //hasil pergeseran
  end;
   Series2.AddXY(X_aksen[1],Y_aksen[1]);

end;




procedure TForm1.FormCreate(Sender: TObject);
begin
  SeriesSumbuX.AddXY(-100,0); //sumbu x
  SeriesSumbuX.AddXY(100,0); //sumbu x

  SeriesSumbuY.AddXY(0,-100); //sumbu y
  SeriesSumbuY.AddXY(0,100); //sumbu y

  StringGrid1.ColWidths[0]:=70;


end;

procedure TForm1.MenuItem1Click(Sender: TObject);
const
sAbout = '|======== Nix Geometry Transformation ========|' + #13#10 +
         '' + #13#10 +
         'Version 1.0 - Build 1 july 2017'+ #13#10 +
         'Created by Lukas Setiawan' + #13#10 +
         'Copyright (c) 2017. All Rights Reserved.' + #13#10 +
         'lukassetiawan@yahoo.com' + #13#10 +
         'my other creation :' + #13#10 +
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



end.

