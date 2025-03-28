object FormODEs: TFormODEs
  Left = 0
  Top = 0
  Caption = 'ODEs Solver by Lukas Setiawan'
  ClientHeight = 729
  ClientWidth = 1344
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label4: TLabel
    Left = 60
    Top = 94
    Width = 17
    Height = 13
    Caption = 'a ='
  end
  object Label5: TLabel
    Left = 69
    Top = 94
    Width = 17
    Height = 13
    Caption = 'a ='
  end
  object Label11: TLabel
    Left = 22
    Top = 227
    Width = 19
    Height = 13
    Caption = 'M ='
  end
  object PageControl1: TPageControl
    Left = 0
    Top = 0
    Width = 1355
    Height = 760
    ActivePage = TabSheet1
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 0
    object TabSheet1: TTabSheet
      Caption = 'First-Order RK4'
      object Label1: TLabel
        Left = 16
        Top = 16
        Width = 76
        Height = 13
        Caption = 'Y '#39' = F(x,y) = '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label2: TLabel
        Left = 65
        Top = 43
        Width = 19
        Height = 13
        Caption = 'a ='
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label7: TLabel
        Left = 39
        Top = 97
        Width = 52
        Height = 13
        Caption = 'Y( x0 ) = '
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label6: TLabel
        Left = 65
        Top = 67
        Width = 19
        Height = 13
        Caption = 'b ='
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label8: TLabel
        Left = 65
        Top = 121
        Width = 22
        Height = 13
        Caption = 'M ='
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label9: TLabel
        Left = 16
        Top = 161
        Width = 275
        Height = 13
        Caption = 'Have an analytic/exact solution(for comparison)'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label10: TLabel
        Left = 16
        Top = 309
        Width = 42
        Height = 13
        Caption = 'Result :'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label12: TLabel
        Left = 19
        Top = 203
        Width = 57
        Height = 13
        Caption = 'Y = F(x) ='
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object Label14: TLabel
        Left = 656
        Top = 360
        Width = 37
        Height = 13
        Caption = 'Label14'
      end
      object Label13: TLabel
        Left = 412
        Top = 10
        Width = 31
        Height = 13
        Caption = 'Help :'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object EditF: TEdit
        Left = 91
        Top = 13
        Width = 262
        Height = 21
        TabOrder = 0
        Text = 'x*exp(3*x)+3*y'
      end
      object EditA: TEdit
        Left = 91
        Top = 40
        Width = 121
        Height = 21
        TabOrder = 1
        Text = '0'
      end
      object EditB: TEdit
        Left = 91
        Top = 67
        Width = 121
        Height = 21
        TabOrder = 2
        Text = '1'
      end
      object EditY0: TEdit
        Left = 91
        Top = 94
        Width = 121
        Height = 21
        TabOrder = 3
        Text = '1'
      end
      object EditM: TEdit
        Left = 91
        Top = 121
        Width = 121
        Height = 21
        TabOrder = 4
        Text = '20'
      end
      object cbExact: TCheckBox
        Left = 16
        Top = 180
        Width = 97
        Height = 17
        Caption = 'Check here'
        Checked = True
        State = cbChecked
        TabOrder = 6
      end
      object ButRun: TButton
        Left = 91
        Top = 249
        Width = 75
        Height = 33
        Caption = 'Run'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 7
        OnClick = ButRunClick
      end
      object ButClear: TButton
        Left = 203
        Top = 249
        Width = 75
        Height = 33
        Caption = 'Clear'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
        TabOrder = 8
        OnClick = ButClearClick
      end
      object MemResult: TMemo
        Left = 16
        Top = 328
        Width = 678
        Height = 281
        ScrollBars = ssBoth
        TabOrder = 9
      end
      object ButAbout: TButton
        Left = 637
        Top = 203
        Width = 57
        Height = 25
        Caption = 'About'
        TabOrder = 10
        OnClick = ButAboutClick
      end
      object EditExact: TEdit
        Left = 91
        Top = 200
        Width = 262
        Height = 21
        TabOrder = 5
        Text = '0.5*x^2*exp(3*x)+exp(3*x)'
      end
      object Memo1: TMemo
        Left = 412
        Top = 34
        Width = 281
        Height = 163
        Lines.Strings = (
          'FIRST-ORDER ODE(IVP)'
          ''
          'First-Order Ordinary Differential Equation (ivp) using '
          'Runge-Kutta 4th Order(RK4) method.'
          ''
          'To approximate the solution of the initial value problem'
          '(ivp) y '#39' =f(x,y) with y(a)=yo over [a,b] and '
          'subinterval M.')
        TabOrder = 11
      end
    end
    object TabSheet2: TTabSheet
      Caption = 'Second-Order RK4'
      ImageIndex = 1
      object Label3: TLabel
        Left = 18
        Top = 16
        Width = 83
        Height = 13
        Caption = 'To be continue...'
      end
    end
  end
  object Chart1: TChart
    Left = 704
    Top = 233
    Width = 642
    Height = 400
    Legend.LegendStyle = lsSeries
    Title.Text.Strings = (
      'TChart')
    BottomAxis.Title.Caption = 'X'
    LeftAxis.Title.Caption = 'Y'
    View3D = False
    TabOrder = 1
    DefaultCanvas = 'TGDIPlusCanvas'
    ColorPaletteIndex = 13
    object Series1: TPointSeries
      SeriesColor = -1
      Title = 'RK4'
      ClickableLine = False
      Pointer.Brush.Color = clRed
      Pointer.Draw3D = False
      Pointer.HorizSize = 3
      Pointer.InflateMargins = True
      Pointer.Shadow.Color = clRed
      Pointer.Style = psCircle
      Pointer.VertSize = 3
      XValues.Name = 'X'
      XValues.Order = loAscending
      YValues.Name = 'Y'
      YValues.Order = loNone
    end
    object Series2: TLineSeries
      SeriesColor = clBlue
      Shadow.Visible = False
      Title = 'Exact '
      Brush.BackColor = clDefault
      Dark3D = False
      Pointer.InflateMargins = True
      Pointer.Style = psRectangle
      XValues.Name = 'X'
      XValues.Order = loAscending
      YValues.Name = 'Y'
      YValues.Order = loNone
    end
  end
  object bcParser1: TbcParser
    Left = 384
    Top = 40
  end
end
