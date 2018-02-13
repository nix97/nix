unit unDet;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, FileUtil, Forms, Controls, Graphics, Dialogs, Grids,
  StdCtrls;

type

  { TForm1 }

  TForm1 = class(TForm)
    ButRun: TButton;
    Button1: TButton;
    ButClear: TButton;
    LabDet: TLabel;
    ordo: TEdit;
    Label1: TLabel;
    sg: TStringGrid;
    procedure ButRunClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure ButClearClick(Sender: TObject);
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

procedure TForm1.Button1Click(Sender: TObject);
var
  i,n:integer;
begin
 n:=strtoint(ordo.text);

 for i:=1 to n do
 begin
   sg.Cells[0,i]:='Baris '+inttostr(i);
   sg.Cells[i,0]:='Kolom '+inttostr(i);
 end;
 sg.Cells[0,0]:='Matrix M';
end;

procedure TForm1.ButClearClick(Sender: TObject);
var i:integer;
begin
  ordo.Clear;
  for i := 0 to sg.rowCount-1 do
   sg.rows[i].Clear;

  labDet.Caption:='Determinant = ?';

end;

procedure TForm1.ButRunClick(Sender: TObject);
var
  i,j,k,n,l:integer;
  M:array[1..100,1..100] of extended;
  ratio,det:extended;
begin

  n:=strtoint(ordo.text);

  for i:=1 to n do
   for j:=1 to n do
   begin
    M[i,j]:=strtofloat(sg.Cells[i,j]);
   end;


  for i:=1 to n do
  begin
    for j:=i+1 to n do
    begin
     ratio:= M[j,i]/M[i,i];
     for k:=1 to n do
      M[j,k]:=M[j,k] - ratio * M[i,k];
  end;
  det:=1;

  for l:=1 to n do
   det:=det*M[l,l];


   labDet.Caption:='Determinant = '+floattostr(det);
 end;

end;

end.

