unit unitConv;

{$mode objfpc}{$H+}

interface

uses
  Math,Classes, SysUtils, FileUtil, Forms, Controls, Graphics, Dialogs, StdCtrls;

type

  { TForm1 }

  TForm1 = class(TForm)
    ButtonAbout: TButton;
    ButtonRunAny: TButton;
    cb3: TComboBox;
    cb4: TComboBox;
    EditAny: TEdit;
    EditAnyRes: TEdit;
    Label11: TLabel;
    Label12: TLabel;
    Label13: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    procedure ButtonAboutClick(Sender: TObject);
    procedure ButtonRunAnyClick(Sender: TObject);
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
procedure BinToDec(var s_in:string;var s_out:string);
var
  n,s2,i,b:longint;
  s,r:array[1..100000] of longint;
 begin
  n:=length(s_in);
  b:=2;
  s2:=0;
   for i:=1 to n do
   begin
    r[i]:=strtoint(s_in[i]);
    n:=n-1;
    s[i]:=r[i]*trunc((power(b,n)));
    s2:=s2+s[i];
   end;

  s_out:=inttostr(s2);
end;

procedure DecToOct(var s_in:string;var s_out:string);
var
  i,j,b,d,n3:integer;
  r:array[1..100000] of integer;

 begin
  n3:=strtoint(s_in);
  b:=8;
  i:=1;
  Repeat
    d:=n3 div b;   // 45/2=22 sisa 1
    r[i]:=n3 mod b;
    n3:=d;
    inc(i);
  until (n3=0);

  s_out:='';

  for j:=i-1 downto 1 do
    s_out:=s_out+inttostr(r[j]);
end;

Procedure DecToHex(var s_in:string;var s_out:string);
var
  i,j,n,b,d:integer;
  r:array[1..100000] of integer;
  p:array[1..100000] of char;

begin
  n:=strtoint(s_in);
  b:=16;

  i:=1;
  Repeat
    d:=n div b;
    r[i]:=n mod b;
    n:=d;
    inc(i);
  until (n=0);

  p[1]:='0';

  s_out:='';

   for j:=i-1 downto 1 do
   begin
   case r[j] of
    0:p[j]:='0';
    1:p[j]:='1';
    2:p[j]:='2';
    3:p[j]:='3';
    4:p[j]:='4';
    5:p[j]:='5';
    6:p[j]:='6';
    7:p[j]:='7';
    8:p[j]:='8';
    9:p[j]:='9';
    10:p[j]:='A';
    11:p[j]:='B';
    12:p[j]:='C';
    13:p[j]:='D';
    14:p[j]:='E';
    15:p[j]:='F';
   end;
   s_out:=s_out+(p[j]);
  end;

end;

procedure OctToDec(var s_in:string;var s_out:string);
var
  n,s2,i,b:longint;
  s,r:array[1..100000] of longint;
begin

  n:=length(s_in);
  b:=8;
  s2:=0;
   for i:=1 to n do
   begin
    r[i]:=strtoint(s_in[i]);
    n:=n-1;
    s[i]:=r[i]*trunc((power(b,n)));
    s2:=s2+s[i];
   end;

  s_out:=inttostr(s2);
end;

procedure HexToDec(var s_in:string;var s_out:string);
var
  n,s2,i,b,j:longint;
  s,r:array[1..100000] of longint;
  n2:string;
begin

   n:=length(s_in);
   n2:=s_in;
   b:=16;

     for j:=1 to n do
     begin
     case n2[j] of
      '0':r[j]:=0;
      '1':r[j]:=1;
      '2':r[j]:=2;
      '3':r[j]:=3;
      '4':r[j]:=4;
      '5':r[j]:=5;
      '6':r[j]:=6;
      '7':r[j]:=7;
      '8':r[j]:=8;
      '9':r[j]:=9;
      'A':r[j]:=10;
      'B':r[j]:=11;
      'C':r[j]:=12;
      'D':r[j]:=13;
      'E':r[j]:=14;
      'F':r[j]:=15;
     end;

   end;

     s2:=0;
     for i:=1 to n do
     begin
      n:=n-1;
      s[i]:=r[i]*trunc((power(b,n)));
      s2:=s2+s[i];
     end;

     s_out:=inttostr(s2);
  end;



procedure DecToBin(var s_in:string;var s_out:string);
var
  i,j,b,d,n3:integer;
  r:array[1..100000] of integer;

 begin
  n3:=strtoint(s_in);
  b:=2;
  i:=1;
  Repeat
    d:=n3 div b;
    r[i]:=n3 mod b;
    n3:=d;
    inc(i);
  until (n3=0);

  s_out:='';

  for j:=i-1 downto 1 do
    s_out:=s_out+inttostr(r[j]);
end;




procedure TForm1.ButtonRunAnyClick(Sender: TObject);

var
  val,n2,hasil,hasil2:string;

begin
  try

  if ((cb3.ItemIndex=0) and (cb4.ItemIndex=0)) then val:='00'
  else if ((cb3.ItemIndex=0) and (cb4.ItemIndex=1)) then val:='01'
  else if ((cb3.ItemIndex=0) and (cb4.ItemIndex=2)) then val:='02'
  else if ((cb3.ItemIndex=0) and (cb4.ItemIndex=3)) then val:='03'
  else if ((cb3.ItemIndex=1) and (cb4.ItemIndex=0)) then val:='10'
  else if ((cb3.ItemIndex=1) and (cb4.ItemIndex=1)) then val:='11'
  else if ((cb3.ItemIndex=1) and (cb4.ItemIndex=2)) then val:='12'
  else if ((cb3.ItemIndex=1) and (cb4.ItemIndex=3)) then val:='13'
  else if ((cb3.ItemIndex=2) and (cb4.ItemIndex=0)) then val:='20'
  else if ((cb3.ItemIndex=2) and (cb4.ItemIndex=1)) then val:='21'
  else if ((cb3.ItemIndex=2) and (cb4.ItemIndex=2)) then val:='22'
  else if ((cb3.ItemIndex=2) and (cb4.ItemIndex=3)) then val:='23'
  else if ((cb3.ItemIndex=3) and (cb4.ItemIndex=0)) then val:='30'
  else if ((cb3.ItemIndex=3) and (cb4.ItemIndex=1)) then val:='31'
  else if ((cb3.ItemIndex=3) and (cb4.ItemIndex=2)) then val:='32'
  else if ((cb3.ItemIndex=3) and (cb4.ItemIndex=3)) then val:='33';


  case val of
  '00': //bin to bin
  begin
   editAnyRes.text:=editAny.text;
  end;

  '01': //bin to octal (2 steps lewat decimal dulu )
  begin
  //bin to decimal
  n2:=editAny.Text;
  hasil:='';
  BinToDec(n2,hasil); //use procedure BinToDec
  //dec to octal
  hasil2:='';
  DecToOct(hasil,hasil2);
  editAnyRes.Text:=hasil2;

  end;

  '02': //bin to dec
  begin
   n2:=editAny.Text;
   hasil:='';
   BinToDec(n2,hasil);
   editAnyRes.Text:=hasil;
  end;
  '03': //Bin to Hexa
  begin
    n2:=editAny.Text;
    hasil:='';
    BinToDec(n2,hasil);
    //dec to hex
    hasil2:='';
    DecToHex(hasil,hasil2);
    editAnyRes.Text:=hasil2;
  end;
  '10': //Oct to Bin
  begin
  //oct to dec
   n2:=editAny.Text;
   hasil:='';
   OctToDec(n2,hasil);
   //dec to bin
   hasil2:='';
   DecToBin(hasil,hasil2);
   editAnyRes.Text:=hasil2;

  end;

  '11': //Oct to Oct
  begin
   editAnyRes.Text:=editAny.Text;
  end;

  '12': //Oct to Dec
  begin
   //oct to dec
   n2:=editAny.Text;
   hasil:='';
   OctToDec(n2,hasil);
   editAnyRes.Text:=hasil;
  end;

  '13': //Oct to Hex
  begin
    //oct to dec
   n2:=editAny.Text;
   hasil:='';
   OctToDec(n2,hasil);
   //dec to hex
    hasil2:='';
    DecToHex(hasil,hasil2);
    editAnyRes.Text:=hasil2;

  end;

  '20': //Dec to Bin
  begin
   //dec to bin
    n2:=editAny.Text;
    hasil:='';
    DecToBin(n2,hasil);
    editAnyRes.Text:=hasil;
  end;

  '21': //Dec to Oct
  begin
   n2:=editAny.Text;
   hasil:='';
   //dec to octal
   DecToOct(n2,hasil);
   editAnyRes.Text:=hasil;
  end;

  '22': //Dec to Dec
  begin
    editAnyRes.Text:=editAny.text;
  end;

  '23': //Dec to Hex
  begin
   n2:=editAny.Text;
   hasil:='';
   DecToHex(n2,hasil);
   editAnyRes.Text:=hasil;
  end;

  '30': //Hex to Bin
  begin
   //hextodec
   n2:=editAny.Text;
   hasil:='';
   HexToDec(n2,hasil);
   //dec to bin
   hasil2:='';
   DecToBin(hasil,hasil2);
   editAnyRes.Text:=hasil2;
  end;


  '31': //Hex to Oct
  begin
   //hextodec
   n2:=editAny.Text;
   hasil:='';
   HexToDec(n2,hasil);

   //dec to oct
    hasil2:='';
    DecToOct(hasil,hasil2);
    editAnyRes.Text:=hasil2;
  end;

  '32': //Hex to Dec
  begin
   //hextodec
   n2:=editAny.Text;
   hasil:='';
   HexToDec(n2,hasil);
   editAnyRes.Text:=hasil;
  end;


  '33': //Hex to Hex
  begin
   editAnyRes.Text:=editAny.text;
  end;
  end;

  except
   showmessage('Something wrong with input !!!');
  end;
end;

procedure TForm1.ButtonAboutClick(Sender: TObject);
const
sAbout = '|======== Nix Number Conversion ========|' + #13#10 +
         '' + #13#10 +
         'Version 1.0 - Build Feb 10, 2018'+ #13#10 +
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
end.

