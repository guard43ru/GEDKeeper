unit GKChartCore;

{$I GEDKeeper.inc}

{.$DEFINE GEN_DEBUG}

interface

uses
  Types, Windows, Messages, Classes, Contnrs, Graphics, Controls,
  GedCom551, GKEngine, GKCommon, GKLists;

type
  TGenealogyChart = class;

  TCustomPerson = class(TObject)
  private
    FBirthDate, FBirthYear: string;
    FChart: TGenealogyChart;
    FDeathDate, FDeathYear: string;
    FDivorced: Boolean;
    FFamily: string;
    FFullName: string;
    FIsDead: Boolean;
    FKinship: string;
    FLifeYears: string;
    FName: string;
    FPatronymic: string;
    FPt: TPoint;
    FRec: TGEDCOMIndividualRecord;
    FSelected: Boolean;
    FSex: TGEDCOMSex;
    FSigns: TChartPersonSigns;
    FHeight: Integer;
    FWidth: Integer;

    procedure Calc();
    function GetRect(): TRect;
    procedure SetKinship(const Value: string);
  protected
    procedure SetPt(const Value: TPoint); virtual;
  public
    function GetPtX: Longint;
    function GetPtY: Longint;
    procedure SetPtX(const Value: Longint);
    procedure SetPtY(const Value: Longint);
    constructor Create(aChart: TGenealogyChart); virtual;
    destructor Destroy; override;

    procedure BuildBy(iRec: TGEDCOMIndividualRecord);
    procedure Draw(); virtual;

    property Height: Integer read FHeight;
    property Width: Integer read FWidth;

    property BirthDate: string read FBirthDate;
    property DeathDate: string read FDeathDate;
    property Divorced: Boolean read FDivorced write FDivorced;
    property Family: string read FFamily;
    property FullName: string read FFullName;
    property IsDead: Boolean read FIsDead;
    property Kinship: string read FKinship write SetKinship;
    property Name: string read FName;
    property Patronymic: string read FPatronymic;
    property Pt: TPoint read FPt write SetPt;
    property PtX: Longint read GetPtX write SetPtX;
    property PtY: Longint read GetPtY write SetPtY;
    property Rec: TGEDCOMIndividualRecord read FRec;
    property Rect: TRect read GetRect;
    property Selected: Boolean read FSelected write FSelected;
    property Sex: TGEDCOMSex read FSex;
    property Signs: TChartPersonSigns read FSigns;
  end;

  TGenealogyChart = class(TObject)
  private
    FCanvas: TCanvas;
    FOptions: TChartOptions;
  public
    property Canvas: TCanvas read FCanvas;
    property Options: TChartOptions read FOptions write FOptions;
  end;

  TPersonList = class;
  TAncestryChart = class;

  TPersonKind = (pkDefault, pkCopy, pkSpouse);

  TPerson = class(TCustomPerson)
  private
    FBaseSpouse: TPerson;
    FChilds: TPersonList;
    FFather: TPerson;
    FGeneration: Integer;
    FKind: TPersonKind;
    FMother: TPerson;
    FSpouses: TPersonList;

    function GetSpouse(Index: Integer): TPerson;
    function GetSpousesCount: Integer;
  protected
  public
    Parent: TPerson;
    KGen: Integer;

    constructor Create(aChart: TGenealogyChart); override;
    destructor Destroy; override;

    procedure AddSpouse(aSpouse: TPerson);

    property BaseSpouse: TPerson read FBaseSpouse write FBaseSpouse;
    property Childs: TPersonList read FChilds;
    property Father: TPerson read FFather write FFather;
    property Generation: Integer read FGeneration write FGeneration;
    property Kind: TPersonKind read FKind write FKind;
    property Mother: TPerson read FMother write FMother;
    property Spouses[Index: Integer]: TPerson read GetSpouse;
    property SpousesCount: Integer read GetSpousesCount;
  end;

  TPersonList = class(TObjectList)
  private
    function GetItem(Index: Integer): TPerson;
    procedure SetItem(Index: Integer; const Value: TPerson);
  public
    property Items[Index: Integer]: TPerson read GetItem write SetItem; default;
  end;

  TChartKind = (ckAncestors, ckDescendants);

  TAncestryChart = class(TGenealogyChart)
  private
    FBitmap: TBitmap;
    FBranchDistance: Integer;
    FDepthLimit: Integer;
    FFilter: TPersonsFilter;
    FKind: TChartKind;
    FLevelDistance: Integer;
    FMargin: Integer;
    FPersons: TPersonList;
    FRoot: TPerson;
    FScale: Integer;
    FSelected: TPerson;
    FShieldState: TShieldState;
    FSpouseDistance: Integer;
    FTree: TGEDCOMTree;
    FTreeBounds: TRect;

    FGenEdge: TPerson;
    FHMax, FWMax: Integer;

    FUseSourcesFilter: Boolean;

    procedure Clear();
    procedure Draw(aPerson: TPerson);
    function FindRelationship(p1, p2: TPerson; aKind: TChartKind): string;
    procedure GenAncestorsChart(aPerson: TGEDCOMIndividualRecord);
    procedure GenDescendantsChart(aPerson: TGEDCOMIndividualRecord);
    procedure SetBitmap(const Value: TBitmap);
    procedure SetSelected(const Value: TPerson);
    procedure Line(aCanvas: TCanvas; X1, Y1, X2, Y2: Integer);
    procedure KinStep(aPerson: TPerson);
    procedure SetScale(const Value: Integer);
    procedure Predef();
  public
    constructor Create;
    destructor Destroy; override;

    procedure GenChart(aPerson: TGEDCOMIndividualRecord; aKind: TChartKind);
    procedure Redraw();
    procedure SelectBy(aX, aY: Integer);

    property UseSourcesFilter: Boolean read FUseSourcesFilter write FUseSourcesFilter;

    property Bitmap: TBitmap read FBitmap write SetBitmap;
    property BranchDistance: Integer read FBranchDistance write FBranchDistance;
    property DepthLimit: Integer read FDepthLimit write FDepthLimit;
    property Filter: TPersonsFilter read FFilter write FFilter;
    property Margin: Integer read FMargin write FMargin;
    property Root: TPerson read FRoot;
    property Scale: Integer read FScale write SetScale;
    property Selected: TPerson read FSelected write SetSelected;
    property ShieldState: TShieldState read FShieldState write FShieldState;
    property Tree: TGEDCOMTree read FTree write FTree;
    property TreeBounds: TRect read FTreeBounds;
  end;

implementation

uses SysUtils, Math, GKMain;

{$R res\gk.res}

var
  SignsData: array [TChartPersonSign] of record
    Name: string;
    Pic: TBitmap;
  end = (
    (Name: 'GEORGE_CROSS'),
    (Name: 'SOLDIER'),
    (Name: 'SOLDIER_FALL'),
    (Name: 'VETERAN_REAR')
  );

procedure InitSigns();
var
  ps: TChartPersonSign;
begin
  for ps := Low(TChartPersonSign) to High(TChartPersonSign) do begin
    {$IFNDEF DELPHI_NET}
    SignsData[ps].Pic := TBitmap.Create;
    SignsData[ps].Pic.LoadFromResourceName(HInstance, SignsData[ps].Name);
    SignsData[ps].Pic.Transparent := True;
    SignsData[ps].Pic.TransparentMode := tmFixed;
    //SignsData[ps].Pic.TransparentColor := Signs[ps].Pic.Canvas.Pixels[0, 0];
    {$ELSE}
    SignsData[ps].Pic := nil;
    {$ENDIF}
  end;
end;

procedure DoneSigns();
var
  ps: TChartPersonSign;
begin
  for ps := Low(TChartPersonSign) to High(TChartPersonSign) do
    SignsData[ps].Pic.Free;
end;

function GetPersonSign(iRec: TGEDCOMIndividualRecord): TChartPersonSigns;
var
  rs: string;
  cps: TChartPersonSign;
  i: Integer;
begin
  Result := [];

  for i := 0 to iRec.UserReferencesCount - 1 do begin
    rs := iRec.UserReferences[i].StringValue;

    for cps := Low(TChartPersonSign) to High(TChartPersonSign) do 
      if (rs = UserRefs[cps].Name)
      then Include(Result, cps);
  end;
end;

{ TCustomPerson }

constructor TCustomPerson.Create(aChart: TGenealogyChart);
begin
  inherited Create();
  FChart := aChart;
end;

destructor TCustomPerson.Destroy;
begin
  inherited Destroy;
end;

procedure TCustomPerson.BuildBy(iRec: TGEDCOMIndividualRecord);
var
  fam, nam, pat: string;
begin
  FRec := iRec;

  if (iRec <> nil) then begin
    GetNameParts(iRec, fam, nam, pat);

    FFamily := fam;
    FName := nam;
    FPatronymic := pat;
    FFullName := nam + ' ' + pat;
    FBirthDate := GetBirthDate(iRec, dfDD_MM_YYYY);
    FDeathDate := GetDeathDate(iRec, dfDD_MM_YYYY);
    FIsDead := not(IsLive(iRec));
    FSex := iRec.Sex;
    FSigns := GetPersonSign(iRec);

    FBirthYear := GetBirthDate(iRec, dfYYYY);
    FDeathYear := GetDeathDate(iRec, dfYYYY);

    FLifeYears := '';
    if (FBirthYear = '')
    then FLifeYears := '[ ?'
    else FLifeYears := '[ ' + FBirthYear;

    if (FDeathYear = '') then begin
      if FIsDead
      then FLifeYears := FLifeYears + ' - ?';
    end else FLifeYears := FLifeYears + ' - ' + FDeathYear;

    FLifeYears := FLifeYears + ' ]';
  end else begin
    FFamily := '';
    FName := '< ? >';
    FPatronymic := '';
    FFullName := FName;
    FBirthDate := '';
    FDeathDate := '';
    FIsDead := False;
    FSex := svNone;
    FSigns := [];
  end;

  Calc();
end;

procedure TCustomPerson.Draw();
var
  canv: TCanvas;

  procedure DrawBorder(rt: TRect; dead: Boolean);
  begin
    case FSex of
      svMale: begin
        if not(dead) then begin
          if FDivorced
          then canv.Brush.Color := FChart.Options.UnHusbandColor
          else canv.Brush.Color := FChart.Options.MaleColor;
        end else canv.Brush.Color := clBlack;
        canv.Rectangle(rt);
      end;

      svFemale: begin
        if not(dead) then begin
          if FDivorced
          then canv.Brush.Color := FChart.Options.UnWifeColor
          else canv.Brush.Color := FChart.Options.FemaleColor;
        end else canv.Brush.Color := clBlack;
        canv.RoundRect(rt.Left, rt.Top, rt.Left + FWidth - 1, rt.Top + FHeight - 1, 13, 13);
      end;

      svNone, svUndetermined: begin
        if not(dead) then begin
          canv.Brush.Color := FChart.Options.UnkSexColor;
        end else canv.Brush.Color := clBlack;
        canv.Rectangle(rt);
      end;
    end;
  end;

var
  rt, dt: TRect;
  h, line, i: Integer;
  cps: TChartPersonSign;
begin
  canv := FChart.Canvas;
  rt := GetRect();
  h := canv.TextHeight('A');

  canv.Pen.Color := clBlack;
  canv.Pen.Width := 1;

  if (FIsDead) then begin
    dt := rt;
    OffsetRect(dt, -2, -2);
    DrawBorder(dt, True);
  end;

  if (FSelected) then begin
    canv.Pen.Width := 2;
    case FSex of
      svMale: canv.Pen.Color := clBlue;//clNavy;
      svFemale: canv.Pen.Color := clRed;//clMaroon;
      svNone, svUndetermined: canv.Pen.Color := clBlack;
    end;
  end;

  DrawBorder(rt, False);

  canv.Pen.Color := clBlack;
  canv.Pen.Width := 1;

  line := 0;
  canv.TextOut(rt.Left + (FWidth - canv.TextWidth(FFamily)) div 2, rt.Top + 10 + (h * line), FFamily);

  if not(FChart.Options.DiffLines) then begin
    Inc(line);
    canv.TextOut(rt.Left + (FWidth - canv.TextWidth(FFullName)) div 2, rt.Top + 10 + (h * line), FFullName);
  end else begin
    Inc(line);
    canv.TextOut(rt.Left + (FWidth - canv.TextWidth(FName)) div 2, rt.Top + 10 + (h * line), FName);
    Inc(line);
    canv.TextOut(rt.Left + (FWidth - canv.TextWidth(FPatronymic)) div 2, rt.Top + 10 + (h * line), FPatronymic);
  end;

  if not(FChart.Options.OnlyYears) then begin
    if (FChart.Options.BirthDateVisible) then begin
      Inc(line);
      canv.TextOut(rt.Left + (FWidth - canv.TextWidth(FBirthDate)) div 2, rt.Top + 10 + (h * line), FBirthDate);
    end;

    if (FChart.Options.DeathDateVisible) then begin
      Inc(line);
      canv.TextOut(rt.Left + (FWidth - canv.TextWidth(FDeathDate)) div 2, rt.Top + 10 + (h * line), FDeathDate);
    end;
  end else begin
    Inc(line);
    canv.TextOut(rt.Left + (FWidth - canv.TextWidth(FLifeYears)) div 2, rt.Top + 10 + (h * line), FLifeYears);
  end;

  if (FChart.Options.Kinship) then begin
    Inc(line);
    canv.TextOut(rt.Left + (FWidth - canv.TextWidth(FKinship)) div 2, rt.Top + 10 + (h * line), FKinship);
  end;

  if (FChart.Options.SignsVisible) and (FSigns <> []) then begin
    i := 0;
    for cps := Low(TChartPersonSign) to High(TChartPersonSign) do
      if (cps in FSigns) then begin
        canv.Draw(rt.Right, rt.Top - 21 + (i * (SignsData[cps].Pic.Height)), SignsData[cps].Pic);
        Inc(i);
      end;
  end;
end;

procedure TCustomPerson.SetPt(const Value: TPoint);
begin
  FPt := Value;
end;

procedure TCustomPerson.SetPtX(const Value: Longint);
begin
  FPt.X := Value;
end;

procedure TCustomPerson.SetPtY(const Value: Longint);
begin
  FPt.Y := Value;
end;

procedure TCustomPerson.SetKinship(const Value: string);
begin
  FKinship := Value;
  Calc();
end;

function TCustomPerson.GetPtX: Longint;
begin
  Result := FPt.X;
end;

function TCustomPerson.GetPtY: Longint;
begin
  Result := FPt.Y;
end;

function TCustomPerson.GetRect(): TRect;
begin
  Result.Left := Pt.X - (FWidth div 2);
  Result.Right := Result.Left + FWidth - 1;

  Result.Top := Pt.Y;
  Result.Bottom := Result.Top + FHeight - 1;
end;

procedure TCustomPerson.Calc();
var
  wt, maxwid, lines: Integer;
begin
  lines := 2;
  maxwid := FChart.Canvas.TextWidth(FFamily);
  if not(FChart.Options.DiffLines) then begin
    wt := FChart.Canvas.TextWidth(FFullName);
    if (maxwid < wt) then maxwid := wt;
  end else begin
    wt := FChart.Canvas.TextWidth(FName);
    if (maxwid < wt) then maxwid := wt;
    wt := FChart.Canvas.TextWidth(FPatronymic);
    if (maxwid < wt) then maxwid := wt;
    Inc(lines);
  end;

  if not(FChart.Options.OnlyYears) then begin
    if (FChart.Options.BirthDateVisible) then begin
      wt := FChart.Canvas.TextWidth(FBirthDate);
      if (maxwid < wt) then maxwid := wt;
      Inc(lines);
    end;

    if (FChart.Options.DeathDateVisible) then begin
      wt := FChart.Canvas.TextWidth(FDeathDate);
      if (maxwid < wt) then maxwid := wt;
      Inc(lines);
    end;
  end else begin
    wt := FChart.Canvas.TextWidth(FLifeYears);
    if (maxwid < wt) then maxwid := wt;
    Inc(lines);
  end;

  if (FChart.Options.Kinship) then begin
    wt := FChart.Canvas.TextWidth(FKinship);
    if (maxwid < wt) then maxwid := wt;
    Inc(lines);
  end;

  FWidth := maxwid + 20;
  FHeight := FChart.Canvas.TextHeight('A') * lines + 20;
end;

{ TPerson }

constructor TPerson.Create(aChart: TGenealogyChart);
begin
  inherited Create(aChart);
  FSpouses := TPersonList.Create(False);
  FChilds := TPersonList.Create(False);
end;

destructor TPerson.Destroy;
begin
  FChilds.Free;
  FSpouses.Free;
  inherited Destroy;
end;

procedure TPerson.AddSpouse(aSpouse: TPerson);
begin
  FSpouses.Add(aSpouse);
end;

function TPerson.GetSpouse(Index: Integer): TPerson;
begin
  Result := FSpouses.GetItem(Index);
end;

function TPerson.GetSpousesCount: Integer;
begin
  Result := FSpouses.Count;
end;

{ TPersonList }

function TPersonList.GetItem(Index: Integer): TPerson;
begin
  {$IFNDEF DELPHI_NET}
  Result := TPerson(inherited GetItem(Index));
  {$ELSE}
  Result := TPerson(inherited Get(Index));
  {$ENDIF}
end;

procedure TPersonList.SetItem(Index: Integer; const Value: TPerson);
begin
  {$IFNDEF DELPHI_NET}
  inherited SetItem(Index, Value);
  {$ELSE}
  inherited Put(Index, Value);
  {$ENDIF}
end;

{ TAncestryChart }

constructor TAncestryChart.Create;
begin
  inherited Create;
  FPersons := TPersonList.Create(True);
  FSpouseDistance := 10;
  FBranchDistance := 40;
  FLevelDistance := 46;
  FMargin := 40;
  FDepthLimit := -1;
  FSelected := nil;
end;

destructor TAncestryChart.Destroy;
begin
  FPersons.Free;
  inherited Destroy;
end;

const
  ACFontSize = 8;
  ACSpouseDistance = 10;
  ACBranchDistance = 40;
  ACLevelDistance = 46;
  ACMargin = 40;

procedure TAncestryChart.Predef();
var
  sc: Single;
  fsz: Integer;
begin
  sc := (FScale / 100);

  fsz := Round(ACFontSize * sc);
  if (fsz <= 7)
  then FCanvas.Font.Name := 'Small Fonts'
  else FCanvas.Font.Name := 'Tahoma';
  FCanvas.Font.Size := fsz;

  FSpouseDistance := Round(ACSpouseDistance * sc);
  FBranchDistance := Round(ACBranchDistance * sc);
  FLevelDistance := Round(ACLevelDistance * sc);
  FMargin := Round(ACMargin * sc);
end;

procedure TAncestryChart.SetBitmap(const Value: TBitmap);
begin
  FBitmap := Value;
  FCanvas := FBitmap.Canvas;
end;

procedure TAncestryChart.KinStep(aPerson: TPerson);
var
  i: Integer;
begin
  if (aPerson = nil) then Exit;

  aPerson.Kinship := FindRelationship(aPerson, FRoot, FKind);

  if (FKind = ckAncestors) then begin
    KinStep(aPerson.Father);
    KinStep(aPerson.Mother);
  end else begin
    for i := 0 to aPerson.Childs.Count - 1 do
      KinStep(aPerson.Childs[i]);
  end;
end;

procedure TAncestryChart.Line(aCanvas: TCanvas; X1, Y1, X2, Y2: Integer);
var
  pc: TColor;
begin
  aCanvas.MoveTo(X1, Y1);
  aCanvas.LineTo(X2, Y2);

  pc := aCanvas.Pen.Color;
  aCanvas.Pen.Color := clSilver;

  if (X1 = X2) then begin
    aCanvas.MoveTo(X1 + 1, Y1 + 1);
    aCanvas.LineTo(X2 + 1, Y2 + 0);
  end
  else
  if (Y1 = Y2) then begin
    aCanvas.MoveTo(X1 + 1, Y1 + 1);
    aCanvas.LineTo(X2 + 0, Y2 + 1);
  end;

  aCanvas.Pen.Color := pc;
end;

procedure TAncestryChart.Draw(aPerson: TPerson);
var
  cr_y, i, bpx, epx, cx, spb_beg, spb_ofs, spb_v: Integer;
  child_pt: TPoint;
begin
  if (aPerson = nil) then Exit;

  if (FKind = ckAncestors) then begin
    Draw(aPerson.Father);
    Draw(aPerson.Mother);

    cr_y := aPerson.Pt.Y - FLevelDistance div 2;

    if (aPerson.Father <> nil) or (aPerson.Mother <> nil) then begin
      Line(FCanvas, aPerson.Pt.X, aPerson.Pt.Y, aPerson.Pt.X, cr_y);
    end;

    if (aPerson.Father <> nil) then begin
      Line(FCanvas, aPerson.Father.Pt.X, cr_y, aPerson.Pt.X, cr_y);
      Line(FCanvas, aPerson.Father.Pt.X, aPerson.Father.Pt.Y + aPerson.Father.Height - 1, aPerson.Father.Pt.X, cr_y + 1);
    end;

    if (aPerson.Mother <> nil) then begin
      Line(FCanvas, aPerson.Pt.X, cr_y, aPerson.Mother.Pt.X, cr_y);
      Line(FCanvas, aPerson.Mother.Pt.X, aPerson.Mother.Pt.Y + aPerson.Mother.Height - 1, aPerson.Mother.Pt.X, cr_y + 1);
    end;
  end else begin
    for i := 0 to aPerson.Childs.Count - 1 do
      Draw(aPerson.Childs[i]);

    spb_ofs := {FLevelDistance div 3} ((aPerson.Height - 10) div (aPerson.SpousesCount + 1));
    spb_beg := aPerson.Pt.Y + (aPerson.Height - spb_ofs * (aPerson.SpousesCount - 1)) div 2;

    case aPerson.Sex of
      svMale: begin
        for i := 0 to aPerson.SpousesCount - 1 do begin
          spb_v := spb_beg + (spb_ofs * i);
          Line(FCanvas, aPerson.Rect.Right, spb_v, aPerson.Spouses[i].Rect.Left, spb_v);
        end;
      end;

      svFemale: begin
        for i := 0 to aPerson.SpousesCount - 1 do begin
          spb_v := spb_beg + (spb_ofs * i);
          Line(FCanvas, aPerson.Spouses[i].Rect.Right, spb_v, aPerson.Rect.Left, spb_v);
        end;
      end;
    end;

    for i := 0 to aPerson.SpousesCount - 1 do
      Draw(aPerson.Spouses[i]);

    cr_y := aPerson.Pt.Y + aPerson.Height + FLevelDistance div 2;

    if (aPerson.BaseSpouse = nil)
    or ((aPerson.BaseSpouse <> nil) and (aPerson.BaseSpouse.SpousesCount > 1))
    then begin
      cx := aPerson.Pt.X;
      spb_beg := aPerson.Pt.Y + aPerson.Height - 1;
    end else begin
      case aPerson.Sex of
        svMale: cx := (aPerson.Rect.Right + aPerson.BaseSpouse.Rect.Left) div 2;
        svFemale: cx := (aPerson.BaseSpouse.Rect.Right + aPerson.Rect.Left) div 2;
      end;

      spb_beg := spb_beg - spb_ofs div 2;
    end;

    if (aPerson.Childs.Count <> 0) then begin
      Line(FCanvas, cx, spb_beg, cx, cr_y); // vert, from fam to childs

      if (aPerson.Childs.Count = 1) then begin
        child_pt := aPerson.Childs[0].Pt;
        Line(FCanvas, child_pt.X, cr_y, child_pt.X, child_pt.Y); // vert, connect from ? to child
      end else begin
        bpx := aPerson.Childs[0].Pt.X;
        epx := aPerson.Childs[aPerson.Childs.Count-1].Pt.X;

        Line(FCanvas, bpx, cr_y, epx, cr_y); // horiz, merge childs

        for i := 0 to aPerson.Childs.Count - 1 do begin
          child_pt := aPerson.Childs[i].Pt;
          Line(FCanvas, child_pt.X, cr_y, child_pt.X, child_pt.Y); // vert
        end;
      end;
    end;
  end;

  aPerson.Draw();
end;

procedure TAncestryChart.GenAncestorsChart(aPerson: TGEDCOMIndividualRecord);

  function Anc_Step(aChild: TPerson; aPerson: TGEDCOMIndividualRecord; aGeneration: Integer): TPerson;
  var
    family: TGEDCOMFamilyRecord;
    iFather, iMother: TGEDCOMIndividualRecord;
    divorced: Boolean;
  begin
    if (aPerson = nil) then begin
      Result := nil;
      Exit;
    end;

    Result := TPerson.Create(Self);
    Result.BuildBy(aPerson);
    Result.Generation := aGeneration;
    Result.Childs.Add(aChild);

    if (FDepthLimit > -1) and (aGeneration = FDepthLimit) then Exit;

    if (aPerson.ChildToFamilyLinksCount > 0) then begin
      family := aPerson.ChildToFamilyLinks[0].Family;

      if (IsRecordAccess(family.Restriction, FShieldState)) then begin
        iFather := TGEDCOMIndividualRecord(family.Husband.Value);
        iMother := TGEDCOMIndividualRecord(family.Wife.Value);

        divorced := (family.TagStringValue('_STAT') = 'NOTMARR');

        if (iFather <> nil) and (IsRecordAccess(iFather.Restriction, FShieldState)) then begin
          Result.Father := Anc_Step(Result, iFather, aGeneration + 1);
          if (Result.Father <> nil)
          then Result.Father.Divorced := divorced;
        end else Result.Father := nil;

        if (iMother <> nil) and (IsRecordAccess(iMother.Restriction, FShieldState)) then begin
          Result.Mother := Anc_Step(Result, iMother, aGeneration + 1);
          if (Result.Mother <> nil)
          then Result.Mother.Divorced := divorced;
        end else Result.Mother := nil;
      end;
    end;
  end;

  function EdgeIsChild(aPerson: TPerson): Boolean;
  var
    p: TPerson;
  begin
    Result := False;

    p := aPerson;
    while (p <> nil) do begin
      if (p = FGenEdge) then begin
        Result := True;
        Exit;
      end;

      if (p.Childs.Count < 1)
      then p := nil
      else p := p.Childs[0];
    end;
  end;

  procedure PrepareIntersect(aPerson: TPerson);
  var
    has: Boolean;
    offset: Integer;
    p: TPerson;
    xpt: TPoint;
  begin
    has := False;

    if (aPerson.Rect.Left < 0 + Margin) then begin
      offset := (0 - aPerson.Rect.Left) + Margin;
      has := True;
    end
    else
    if (FGenEdge <> nil) then begin
      has := (aPerson.Rect.Left <= FGenEdge.Rect.Right + FBranchDistance) and not(EdgeIsChild(aPerson));

      if has
      then offset := ((FGenEdge.Rect.Right + FBranchDistance) - aPerson.Rect.Left);
    end;

    if has then begin
      p := aPerson;
      while (p <> nil) do begin
        xpt := p.Pt;
        xpt.X := xpt.X + offset;
        p.Pt := xpt;

        if (p.Childs.Count < 1)
        then p := nil
        else p := p.Childs[0];
      end;
    end;
  end;

  procedure YPrepareIntersect(aPerson: TPerson);
  var
    offset, i: Integer;
    xpt: TPoint;
  begin
    if (aPerson.Rect.Top < 0) then begin
      offset := (0 - aPerson.Rect.Top) + Margin;
      for i := 0 to FPersons.Count - 1 do begin
        xpt := FPersons[i].Pt;
        xpt.Y := xpt.Y + offset;
        FPersons[i].Pt := xpt;
      end;
    end;
  end;

  procedure Recalc(aPerson: TPerson; aPt: TPoint);
  var
    pw: Integer;
    xpt: TPoint;
    both: Boolean;
  begin
    if (aPerson = nil) then Exit;

    aPerson.Pt := aPt;

    PrepareIntersect(aPerson);

    FPersons.Add(aPerson);

    YPrepareIntersect(aPerson);

    both := (aPerson.Father <> nil) and (aPerson.Mother <> nil);

    if (both) then begin
      pw := 0;
      if (both) then pw := aPerson.Father.Width;
      xpt := Point(aPerson.Pt.X - (FSpouseDistance + (pw div 2)),
                   aPerson.Pt.Y - FLevelDistance - aPerson.Height);
      Recalc(aPerson.Father, xpt);

      pw := 0;
      if (both) then pw := aPerson.Mother.Width;
      xpt := Point(aPerson.Pt.X + (FSpouseDistance + (pw div 2)),
                   aPerson.Pt.Y - FLevelDistance - aPerson.Height);
      Recalc(aPerson.Mother, xpt);
    end else begin
      xpt := Point(aPerson.Pt.X, aPerson.Pt.Y - FLevelDistance - aPerson.Height);

      if (aPerson.Father <> nil) then Recalc(aPerson.Father, xpt)
      else
      if (aPerson.Mother <> nil) then Recalc(aPerson.Mother, xpt);
    end;

    if (both) then begin
      xpt := aPerson.Pt;
      xpt.X := aPerson.Father.Pt.X + (aPerson.Mother.Pt.X - aPerson.Father.Pt.X) div 2;
      aPerson.Pt := xpt;
    end;

    if (FGenEdge = nil) then begin
      if (aPerson <> FRoot)
      then FGenEdge := aPerson;
    end else begin
      if (aPerson.Rect.Right > FGenEdge.Rect.Right)
      then FGenEdge := aPerson;
    end;

    if (FWMax < aPerson.Rect.Right) then FWMax := aPerson.Rect.Right;
    if (FHMax < aPerson.Rect.Bottom) then FHMax := aPerson.Rect.Bottom;
  end;

begin
  FRoot := Anc_Step(nil, aPerson, 1);

  if (FOptions.Kinship) then KinStep(FRoot);

  Recalc(FRoot, Point(FMargin, FMargin));
end;

procedure TAncestryChart.GenDescendantsChart(aPerson: TGEDCOMIndividualRecord);

  function AddPerson(aParent: TPerson; iRec: TGEDCOMIndividualRecord;
    aKind: TPersonKind; aGeneration: Integer): TPerson;
  begin
    Result := TPerson.Create(Self);
    Result.BuildBy(iRec);
    Result.Generation := aGeneration;
    Result.Parent := aParent;
    Result.Kind := aKind;
    FPersons.Add(Result);

    if (aKind = pkSpouse) then Exit;

    if (aParent = nil) then begin
      if (FRoot = nil) then FRoot := Result;
    end else aParent.Childs.Add(Result);
  end;

  function IsChildless(iRec: TGEDCOMIndividualRecord): Boolean;
  var
    exp: string;
  begin
    exp := GetLifeExpectancy(iRec);
    if (exp = '') or (exp = '?')
    then Result := False
    else Result := (StrToInt(exp) < 15);
  end;

  procedure Desc_Step(aParent: TPerson; aPerson: TGEDCOMIndividualRecord; aLevel: Integer);
  var
    res, res_parent: TPerson;
    family: TGEDCOMFamilyRecord;
    child, sp: TGEDCOMIndividualRecord;
    i, k: Integer;
    filter_source: TGEDCOMSourceRecord;
  begin
    if (aPerson = nil) then Exit;

    if (FOptions.ChildlessExclude) and (aLevel > 1) then begin
      if (aPerson.SpouseToFamilyLinksCount = 0) and IsChildless(aPerson)
      then Exit;
    end;

    if (FUseSourcesFilter) and (FFilter <> nil) then begin
      case FFilter.SourceMode of
        gmAll: ;
        gmNone: if (aPerson.SourceCitationsCount <> 0) then Exit;
        gmAny: if (aPerson.SourceCitationsCount = 0) then Exit;
        gmSelected: begin
          if (FFilter.SourceRef = '')
          then filter_source := nil
          else filter_source := FTree.XRefIndex_Find(FFilter.SourceRef) as TGEDCOMSourceRecord;

          if (aPerson.IndexOfSource(filter_source) < 0) then Exit;
        end;
      end;
    end;

    res := AddPerson(aParent, aPerson, pkDefault, aLevel);

    for k := 0 to aPerson.SpouseToFamilyLinksCount - 1 do begin
      family := aPerson.SpouseToFamilyLinks[k].Family;
      if not(IsRecordAccess(family.Restriction, FShieldState)) then Continue;

      res_parent := nil;
      case aPerson.Sex of
        svMale: begin
          sp := TGEDCOMIndividualRecord(family.Wife.Value);
          res_parent := AddPerson(nil, sp, pkSpouse, aLevel);
          res_parent.FSex := svFemale;
        end;
        svFemale: begin
          sp := TGEDCOMIndividualRecord(family.Husband.Value);
          res_parent := AddPerson(nil, sp, pkSpouse, aLevel);
          res_parent.FSex := svMale;
        end;
      end;

      if (res_parent <> nil) then begin
        res.AddSpouse(res_parent);
        res_parent.BaseSpouse := res;
      end else res_parent := res;

      if (FDepthLimit > -1) and (aLevel = FDepthLimit) then Continue;

      for i := 0 to family.ChildrenCount - 1 do begin
        child := TGEDCOMIndividualRecord(family.Children[i].Value);
        if not(IsRecordAccess(child.Restriction, FShieldState)) then Continue;

        Desc_Step(res_parent, child, aLevel + 1);
      end;
    end;
  end;

  function EdgeIsParent(aPerson: TPerson): Boolean;
  var
    p: TPerson;
  begin
    Result := False;

    p := aPerson;
    while (p <> nil) do begin
      if (p = FGenEdge) then begin
        Result := True;
        Exit;
      end;

      p := p.Parent;
    end;
  end;

  procedure PrepareIntersect(aPerson: TPerson);
  var
    has: Boolean;
    offset: Integer;
    p: TPerson;
    xpt: TPoint;
  begin
    has := False;

    if (FGenEdge <> nil) then begin
      has := (aPerson.Rect.Left <= FGenEdge.Rect.Right + FBranchDistance) and not(EdgeIsParent(aPerson));

      if has
      then offset := ((FGenEdge.Rect.Right + FBranchDistance) - aPerson.Rect.Left);
    end else begin
      if (aPerson.Rect.Left < 0 + FMargin) then begin
        offset := (0 - aPerson.Rect.Left) + FMargin;
        has := True;
      end;
    end;

    if has then begin
      p := aPerson;
      while (p <> nil) do begin
        xpt := p.Pt;
        xpt.X := xpt.X + offset;
        p.Pt := xpt;

        if (p.BaseSpouse <> nil)
        and ((p.BaseSpouse.Sex = svFemale) or ({(p.BaseSpouse.Sex = svMale) and }(p.BaseSpouse.SpousesCount = 1)))
        then p := p.BaseSpouse
        else p := p.Parent;
      end;
    end;
  end;

  procedure Desc_Recalc(aPerson: TPerson; aPt: TPoint);

    procedure RecalcChilds();
    var
      childs_width, i, cur_x, cur_y: Integer;
      child: TPerson;
    begin
      if (aPerson.Childs.Count = 0) then Exit;

      if (aPerson.BaseSpouse = nil)
      or ((aPerson.BaseSpouse <> nil) and (aPerson.BaseSpouse.SpousesCount > 1))
      then cur_x := aPerson.Pt.X
      else begin
        case aPerson.Sex of
          svMale: cur_x := (aPerson.Rect.Right + aPerson.BaseSpouse.Rect.Left) div 2;
          svFemale: cur_x := (aPerson.BaseSpouse.Rect.Right + aPerson.Rect.Left) div 2;
        end;
      end;

      cur_y := aPerson.Pt.Y + FLevelDistance + aPerson.Height;

      if (aPerson.Childs.Count = 1) then begin
        Desc_Recalc(aPerson.Childs[0], Point(cur_x, cur_y));
      end else begin
        childs_width := (aPerson.Childs.Count - 1) * FBranchDistance;
        for i := 0 to aPerson.Childs.Count - 1 do begin
          childs_width := childs_width + aPerson.Childs[i].Width;
        end;

        cur_x := cur_x - (childs_width div 2);

        for i := 0 to aPerson.Childs.Count - 1 do begin
          child := aPerson.Childs[i];

          Desc_Recalc(child, Point(cur_x + (child.Width div 2), cur_y));
          cur_x := child.Rect.Right + FBranchDistance;
        end;
      end;

      // adjusting
      cur_x := aPerson.Childs[0].Pt.X;

      if (aPerson.Childs.Count > 1)
      then cur_x := cur_x + ((aPerson.Childs[aPerson.Childs.Count - 1].Pt.X - cur_x) div 2);

      if ((aPerson.SpousesCount = 1) or ((aPerson.BaseSpouse <> nil) and (aPerson.BaseSpouse.SpousesCount = 1)))
      then begin
        case aPerson.Sex of
          svMale: begin
            aPerson.PtX := cur_x - BranchDistance div 2 - aPerson.Width div 2 + 1;
            aPerson.BaseSpouse.PtX := cur_x + BranchDistance div 2 + aPerson.BaseSpouse.Width div 2;
          end;
          svFemale: begin
            aPerson.PtX := cur_x + BranchDistance div 2 + aPerson.Width div 2;
            aPerson.BaseSpouse.PtX := cur_x - BranchDistance div 2 - aPerson.BaseSpouse.Width div 2 + 1;
          end;
        end;
      end else begin
        aPerson.PtX := cur_x;
      end;
    end;

    procedure RecalcSpouses();
    var
      i: Integer;
      sp: TPerson;
      sp_pt: TPoint;
      prev: TPerson;
    begin
      if (aPerson.SpousesCount = 0) then Exit;

      prev := aPerson;
      for i := 0 to aPerson.SpousesCount - 1 do begin
        sp := aPerson.Spouses[i];

        case aPerson.Sex of
          svMale: sp_pt := Point(prev.Rect.Right + (FBranchDistance + (sp.Width div 2)), aPerson.Pt.Y);
          svFemale: sp_pt := Point(prev.Rect.Left - (FBranchDistance + (sp.Width div 2)), aPerson.Pt.Y);
        end;

        Desc_Recalc(sp, sp_pt);

        if (sp.Sex <> svMale)
        then prev := sp;
      end;
    end;

  begin
    if (aPerson = nil) then Exit;

    aPerson.Pt := aPt;
    PrepareIntersect(aPerson);

    case aPerson.Sex of
      svMale: begin
        RecalcChilds();
        RecalcSpouses();
      end;

      svFemale: begin
        RecalcSpouses();
        RecalcChilds();
      end;
    end;

    if (FGenEdge = nil) then begin
      if (aPerson <> FRoot)
      then FGenEdge := aPerson;
    end else begin
      if (aPerson.Rect.Right > FGenEdge.Rect.Right)
      then FGenEdge := aPerson;
    end;

    if (FWMax < aPerson.Rect.Right) then FWMax := aPerson.Rect.Right;
    if (FHMax < aPerson.Rect.Bottom) then FHMax := aPerson.Rect.Bottom;
  end;

begin
  FRoot := nil;
  Desc_Step(nil, aPerson, 1);

  if (FOptions.Kinship) then KinStep(FRoot);

  Desc_Recalc(FRoot, Point(FMargin, FMargin));
end;

procedure TAncestryChart.GenChart(aPerson: TGEDCOMIndividualRecord; aKind: TChartKind);
begin
  FKind := aKind;

  Clear();

  FHMax := 0;
  FWMax := 0;
  FGenEdge := nil;

  Predef();

  case FKind of
    ckAncestors: GenAncestorsChart(aPerson);
    ckDescendants: GenDescendantsChart(aPerson);
  end;

  FTreeBounds := Rect(0, 0, FWMax + FMargin - 1, FHMax + FMargin - 1);

  Redraw();
end;

procedure TAncestryChart.Redraw();
begin
  FBitmap.Height := FTreeBounds.Bottom - FTreeBounds.Top + 1;
  FBitmap.Width := FTreeBounds.Right - FTreeBounds.Left + 1;
  FBitmap.Canvas.Brush.Color := clWhite;
  FBitmap.Canvas.Rectangle(0, 0, FBitmap.Width, FBitmap.Height);
  Draw(FRoot);
end;

procedure TAncestryChart.SelectBy(aX, aY: Integer);
var
  i: Integer;
  p: TPerson;
begin
  for i := 0 to FPersons.Count - 1 do begin
    p := FPersons[i];

    if PtInRect(p.Rect, Point(aX, aY)) then begin
      SetSelected(p);
      Exit;
    end;
  end;

  SetSelected(nil);
end;

procedure TAncestryChart.Clear();
begin
  FSelected := nil;
  FPersons.Clear;
end;

procedure TAncestryChart.SetSelected(const Value: TPerson);
begin
  if (FSelected <> nil) then FSelected.Selected := False;
  FSelected := Value;
  if (FSelected <> nil) then FSelected.Selected := True;

  Redraw();
end;

// Portions Copyright (C) 2000 Gann Bierner
// Project: Kinsight

type
  TRelationships = array [0..3, 0..3] of string;

const
  male_relationships: TRelationships = (
    ('-',        '���',       '����',    '�������'),
    ('����',    '����',  '���������', 'grand-���������'),
    ('���',     '����', '1st �����', '1st �����, once removed'),
    ('������', 'great-����', '1st �����, once removed', '2nd �����')
  );

  female_relationships: TRelationships = (
    ('-',       '����',     '������',   '���������'),
    ('����',  '������', '����������', 'grand-����������'),
    ('�������', '����', '1st ������', '1st ������, once removed'),
    ('����������', 'great-����', '1st ������, once removed', '2nd ������')
  );

function order(x: Integer): string;
begin
  if (x=1) then Result := '1st'
  else if (x=2) then Result := '2nd'
  else if (x=3) then Result := '3rd'
  else Result := IntToStr(x)+'th';
end;

function great(n: Integer): string;
var
  i: Integer;
begin
  Result := '';

  for i := 1 to n do
    Result := Result + '���';
end;

function times(x: Integer): string;
begin
  if (x = 1) then Result := 'once'
  else if (x = 2) then Result := 'twice'
  else Result := IntToStr(x) + ' times';
end;

procedure getAncestors(p: TPerson; H: TObjectList; gen: Integer);
begin
  p.KGen := gen;
  H.Add(p);

  if (p.Father <> nil) then getAncestors(p.Father, H, gen + 1);
  if (p.Mother <> nil) then getAncestors(p.Mother, H, gen + 1);
end;

procedure getDescendants(p: TPerson; H: TObjectList; gen: Integer);
var
  i: Integer;
begin
  p.KGen := gen;
  H.Add(p);

  for i := 0 to p.Childs.Count - 1 do
    getDescendants(p.Childs[i], H, gen + 1);
end;

function GetPersonGen(p: TPerson; H: TList): Integer;
var
  i: Integer;
begin
  Result := -1;

  for i := 0 to H.Count - 1 do begin
    if (H[i] = p) then begin
      Result := TPerson(H[i]).KGen;
      Break;
    end;
  end;
end;

function findCommonAncestor(p: TPerson; H: TList; gen: Integer; aKind: TChartKind;
  var rgen1, rgen2: Integer): Boolean;
var
  i, gen2: Integer;
begin
  gen2 := GetPersonGen(p, H);
  if (gen2 <> -1) then begin
    Result := True;
    rgen1 := gen;
    rgen2 := gen2;
    Exit;
  end;

  case aKind of
    ckAncestors: begin
      if (p.Father <> nil) then begin
        Result := findCommonAncestor(p.Father, H, gen + 1, aKind, rgen1, rgen2);
        if (Result) then Exit;
      end;

      if (p.Mother <> nil) then begin
        Result := findCommonAncestor(p.Mother, H, gen + 1, aKind, rgen1, rgen2);
        if (Result) then Exit;
      end;
    end;

    ckDescendants: begin
      for i := 0 to p.Childs.Count - 1 do begin
        Result := findCommonAncestor(p.Childs[i], H, gen + 1, aKind, rgen1, rgen2);
        if (Result) then Exit;
      end;
    end;
  end;

  Result := False;
end;

function TAncestryChart.FindRelationship(p1, p2: TPerson; aKind: TChartKind): string;
var
  rel, base: string;
  relationships: TRelationships;
  x, y, diff, gen1, gen2, tmp: Integer;
  H: TObjectList;
  res: Boolean;
begin
  H := TObjectList.Create(False);
  try
    case aKind of
      ckAncestors: getAncestors({p1}FRoot, H, 0);
      ckDescendants: getDescendants({p1}FRoot, H, 0);
    end;

    res := findCommonAncestor({p2}p1, H, 0, aKind, gen1, gen2);
    if not(res) then begin
      Result := '?';
      Exit;
    end;

    case aKind of
      ckAncestors: begin
        tmp := gen1;
        gen1 := gen2;
        gen2 := tmp;
      end;

      ckDescendants: begin
      end;
    end;

    if (p1.Sex = svMale)
    then relationships := male_relationships
    else relationships := female_relationships;

    if (gen1 < 4) and (gen2 < 4)
    then rel := relationships[gen1, gen2]
    else
    if (gen1 = gen2)
    then rel := order(gen1-1) + ' cousin'
    else
    if (gen1 < 2) or (gen2 < 2) then begin
      if (gen1 < 2) then begin
        base := relationships[gen1, 3];
        diff := gen2 - 3;
      end else begin
        base := relationships[3, gen2];
        diff := gen1 - 3;
      end;

      rel := great(diff) + base;
    end else begin
      x := Min(gen1, gen2);
      y := Max(gen1, gen2);
      rel := order(x-1) + ' cousin ' + times(y-x) + ' removed';
    end;

    Result := '[' + rel + ']';
  finally
    H.Free;
  end;
end;

procedure TAncestryChart.SetScale(const Value: Integer);
begin
  FScale := Value;
end;

initialization
  InitSigns();

finalization
  DoneSigns();

end.
