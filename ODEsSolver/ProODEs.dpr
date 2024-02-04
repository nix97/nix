program ProODEs;

uses
  Vcl.Forms,
  UnODEs in 'UnODEs.pas' {FormODEs},
  bcParser in 'bcParser.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TFormODEs, FormODEs);
  Application.Run;
end.
