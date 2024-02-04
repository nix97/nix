unit unitPoly;

{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, FileUtil, Forms, Controls, Graphics, Dialogs, Grids,
  StdCtrls;

type

  { TForm1 }

  TForm1 = class(TForm)
    ButActive: TButton;
    ButSample: TButton;
    ButRun: TButton;
    ButClear: TButton;
    ButAbout: TButton;
    EditRemainder: TEdit;
    EditQuotient: TEdit;
    EditD: TEdit;
    EditN: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Memo1: TMemo;
    sg: TStringGrid;
    procedure ButAboutClick(Sender: TObject);
    procedure ButActiveClick(Sender: TObject);
    procedure ButClearClick(Sender: TObject);
    procedure ButRunClick(Sender: TObject);
    procedure ButSampleClick(Sender: TObject);
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

procedure TForm1.ButActiveClick(Sender: TObject);
label
   akhir;
var
  N,D,i:integer;
begin
  N:=strtoint(editN.text);
  D:=strtoint(editD.text);

  if D>N then
  begin
   showmessage('Degree of Divisor must be lower or equal than Degree of Dividend !!!');
   goto akhir;
  end;

  if (D<0) then
  begin
     showmessage('Degree of Divisor minimum is 0 !!!');
     goto akhir;
  end;

  sg.ColWidths[0]:=125;
  sg.Cells[0,0]:='Input Coeff Dividend :';
  for i:=N downto 0 do
  begin
    sg.Cells[N-i+1,0]:='x^'+floattostr(i);
   end;

  sg.Cells[0,3]:='Input Coeff Divisor :';
  for i:=D downto 0 do
  begin
    sg.Cells[D-i+1,3]:='x^'+floattostr(i);
   end;

 akhir:

end;

procedure TForm1.ButAboutClick(Sender: TObject);
const
 sAbout = '|======== Polynomial Division ========|' + #13#10 +
          '' + #13#10 +
          'Version 1.0 - build march 18, 2018'+ #13#10 +
          'Created by Lukas Setiawan.' + #13#10 +
          'Copyright (c) 2017. All Rights Reserved.' + #13#10 +
          '' + #13#10 +
          'Visit www.metodenumeriku.blogspot.com' + #13#10 +
          'FB search: Metode Numerik-Plus Programnya' + #13#10 +
          'lukassetiawan@yahoo.com' + #13#10 +
          'My other works :'+ #13#10 +
          'https://bitbucket.org/nixz97/nix/downloads/' + #13#10 +

          '' + #13#10 +
          'Educational backgroud :' + #13#10 +
          '1. SD IPPOR Eretan Wetan-Kandanghaur-Indramayu' + #13#10 +
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

procedure TForm1.ButClearClick(Sender: TObject);
var
   i:integer;
begin
  editN.Clear;
  editD.clear;
  editQuotient.clear;
  editRemainder.Clear;
  for i := 0 to sg.rowCount-1 do
   sg.rows[i].Clear;
end;

procedure TForm1.ButRunClick(Sender: TObject);
label
  akhir;
var
  N,D,D2,i,q,r,pjQ,pjR:integer;
  CoeffN,CoeffD,CoeffD2,CoeffQ,CoeffR:array[0..100] of extended;
  quo,quo2,rem,rem2:string;
begin
  N:=strtoint(editN.text);
  D:=strtoint(editD.text);

  if D>N then
  begin
   showmessage('Degree of Divisor must be lower or equal than Degree of Dividend !!!');
   goto akhir;
  end;

  q:=N-D;
  r:=N-D;


  for i:=N downto 0 do
  begin
   CoeffN[i]:=strtofloat(sg.cells[N-i+1,1]);
  end;

  for i:=D downto 0 do
  begin
   CoeffD[i]:=strtofloat(sg.cells[D-i+1,4]);
  end;


  for i:=D+1 to N do
   CoeffD[i]:=0;

  for i:=0 to Q do
   CoeffQ[i]:=0;

  for i:=0 to r do
   CoeffR[i]:=0;

  if (D<0) then
  begin
     showmessage('Degree of Divisor minimum is 0 !!!');
     goto akhir;
  end;


  if (N>=D) then
  begin
   while(N>=D) do
   begin
    for i:=0 to N do
     CoeffD2[i]:=0;

    for i:=0 to D do
     CoeffD2[i+N-D]:=CoeffD[i];

    D2:=N;


 // calculating one element of q
  CoeffQ[N-D]:=(CoeffN[N]/coeffD2[D2]);//D2=dd
 // d equals d * q[dN-dD]
  for i:=0 to Q do
   CoeffD2[i]:= CoeffD2[i]*CoeffQ[N-D];

// N equals N - d
  for i:=0 to N do
   CoeffN[i]:=CoeffN[i]-CoeffD2[i];

   dec(N);

  end;
 end;

  // r equals N
  for i:=0 to N do
   CoeffR[i]:=CoeffN[i];

  R:=N;

 sg.cells[0,6]:='Quotient :';
 for i:=q downto 0 do
 begin
  sg.cells[q-i+1,6]:='x^'+inttostr(i);
  sg.cells[q-i+1,7]:=floattostr(CoeffQ[i]);
 end;

 sg.cells[0,9]:='Remainder :';
 for i:=r downto 0 do
 begin
  sg.cells[r-i+1,9]:='x^'+inttostr(i);
  sg.cells[r-i+1,10]:=floattostr(CoeffR[i]);
 end;

 quo:='';
 for i:=q downto 0 do
  quo:=quo+'('+floattostr(CoeffQ[i])+') x^'+inttostr(i)+' + ';

 rem:='';
 for i:=r downto 0 do
  rem:=rem+'('+floattostr(CoeffR[i])+') x^'+inttostr(i)+' + ';

 pjQ:=length(quo);
 quo2:='';
 for i:=1 to pjQ-2 do
   quo2:=quo2+quo[i];
 EditQuotient.Text:=quo2;


 pjR:=length(rem);
 rem2:='';
 for i:=1 to pjR-2 do
   rem2:=rem2+rem[i];
 EditRemainder.Text:=rem2;

 akhir:

end;

procedure TForm1.ButSampleClick(Sender: TObject);
var
  N,D,i:integer;

begin
  editN.text:='4';
  editD.text:='2';

  N:=strtoint(editN.text);
  D:=strtoint(editD.text);

  sg.ColWidths[0]:=125;
  sg.Cells[0,0]:='Input Coeff Dividend :';
  for i:=N downto 0 do
  begin
    sg.Cells[N-i+1,0]:='x^'+inttostr(i);
  end;

  sg.Cells[1,1]:='1';
  sg.Cells[2,1]:='0';
  sg.Cells[3,1]:='-3';
  sg.Cells[4,1]:='4';
  sg.Cells[5,1]:='5';

  sg.Cells[0,3]:='Input Coeff Divisor :';
  for i:=D downto 0 do
  begin
    sg.Cells[D-i+1,3]:='x^'+inttostr(i);
  end;

  sg.Cells[1,4]:='1';
  sg.Cells[2,4]:='-1';
  sg.Cells[3,4]:='1';


end;

end.

