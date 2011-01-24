unit GKSourceEdit;

{$I GEDKeeper.inc}

interface

uses
  SysUtils, Classes, Controls, Forms, Dialogs, StdCtrls, Buttons, ComCtrls,
  GedCom551, GKBase, GKEngine, GKLists, bsCtrls;

type
  TfmSourceEdit = class(TForm)
    btnAccept: TBitBtn;
    btnCancel: TBitBtn;
    PagesData: TPageControl;
    SheetNotes: TTabSheet;
    SheetMultimedia: TTabSheet;
    SheetRepositories: TTabSheet;
    SheetText: TTabSheet;
    EditText: TMemo;
    SheetCommon: TTabSheet;
    Label1: TLabel;
    EditShortTitle: TEdit;
    Label3: TLabel;
    EditAuthor: TMemo;
    Label2: TLabel;
    EditTitle: TMemo;
    Label4: TLabel;
    EditPublication: TMemo;
    procedure btnAcceptClick(Sender: TObject);
    procedure EditShortTitleChange(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    FSourceRecord: TGEDCOMSourceRecord;

    FNotesList: TSheetList;
    FMediaList: TSheetList;
    FRepositoriesList: TSheetList;

    procedure AcceptChanges();
    procedure ControlsRefresh();
    function GetBase: TfmBase;
    procedure ListModify(Sender: TObject; ItemData: TObject; Action: TRecAction);
    procedure SetSourceRecord(const Value: TGEDCOMSourceRecord);
  public
    property Base: TfmBase read GetBase;
    property SourceRecord: TGEDCOMSourceRecord read FSourceRecord write SetSourceRecord;
  end;

implementation

uses GKMain, GKRecordSelect;

{$R *.dfm}

procedure TfmSourceEdit.FormCreate(Sender: TObject);
begin
  FNotesList := TSheetList.Create(SheetNotes, lmBox);
  FNotesList.OnModify := ListModify;
  Base.SetupRecNotesList(FNotesList);

  FMediaList := TSheetList.Create(SheetMultimedia);
  FMediaList.OnModify := ListModify;
  Base.SetupRecMediaList(FMediaList);

  FRepositoriesList := TSheetList.Create(SheetRepositories);
  FRepositoriesList.OnModify := ListModify;
  FRepositoriesList.Buttons := [lbAdd..lbJump];
  AddListColumn(FRepositoriesList.List, '�����', 300);
end;

procedure TfmSourceEdit.ControlsRefresh();
var
  k: Integer;
  rep: TGEDCOMRepositoryRecord;
begin
  Base.RecListNotesRefresh(FSourceRecord, FNotesList.List, nil);
  Base.RecListMediaRefresh(FSourceRecord, TBSListView(FMediaList.List), nil);

  with TListView(FRepositoriesList.List) do begin
    Items.BeginUpdate();
    Items.Clear();
    for k := 0 to FSourceRecord.RepositoryCitationsCount - 1 do begin
      rep := TGEDCOMRepositoryRecord(FSourceRecord.RepositoryCitations[k].Value);
      AddItem(rep.RepositoryName, FSourceRecord.RepositoryCitations[k]);
    end;
    Items.EndUpdate();
  end;
end;

procedure TfmSourceEdit.SetSourceRecord(const Value: TGEDCOMSourceRecord);
begin
  FSourceRecord := Value;

  EditShortTitle.Text := FSourceRecord.FiledByEntry;
  EditAuthor.Text := Trim(FSourceRecord.Originator.Text);
  EditTitle.Text := Trim(FSourceRecord.Title.Text);
  EditPublication.Text := Trim(FSourceRecord.Publication.Text);
  EditText.Text := Trim(FSourceRecord.Text.Text);

  ControlsRefresh();
end;

procedure TfmSourceEdit.AcceptChanges();
begin
  FSourceRecord.FiledByEntry := EditShortTitle.Text;

  FSourceRecord.Originator.Clear;
  FSourceRecord.Originator := EditAuthor.Lines;

  FSourceRecord.Title.Clear;
  FSourceRecord.Title := EditTitle.Lines;

  FSourceRecord.Publication.Clear;
  FSourceRecord.Publication := EditPublication.Lines;

  FSourceRecord.Text.Clear;
  FSourceRecord.Text := EditText.Lines;

  Base.ChangeRecord(FSourceRecord);
end;

procedure TfmSourceEdit.btnAcceptClick(Sender: TObject);
begin
  AcceptChanges();
end;

procedure TfmSourceEdit.ListModify(Sender: TObject; ItemData: TObject; Action: TRecAction);
var
  rep: TGEDCOMRepositoryRecord;
  cit: TGEDCOMRepositoryCitation;
begin
  if (Sender = FNotesList) then begin
    if Base.ModifyRecNote(Self, FSourceRecord, TGEDCOMNotes(ItemData), Action)
    then ControlsRefresh();
  end
  else
  if (Sender = FMediaList) then begin
    if Base.ModifyRecMultimedia(Self, FSourceRecord, TGEDCOMMultimediaLink(ItemData), Action)
    then ControlsRefresh();
  end
  else
  if (Sender = FRepositoriesList) then begin
    case Action of
      raAdd: begin
        rep := TGEDCOMRepositoryRecord(Base.SelectRecord(rtRepository, []));
        if (rep <> nil) then begin
          BindSourceRepository(Base.Tree, FSourceRecord, rep);
          ControlsRefresh();
        end;
      end;

      raEdit: ;

      raDelete: begin
        cit := TGEDCOMRepositoryCitation(ItemData);

        if (cit = nil) or (MessageDlg('������� ������ �� �����?', mtConfirmation, [mbNo, mbYes], 0) = mrNo)
        then Exit;

        FSourceRecord.DeleteRepositoryCitation(cit);

        ControlsRefresh();
      end;

      raJump: begin
        cit := TGEDCOMRepositoryCitation(ItemData);
        if (cit <> nil) then begin
          AcceptChanges();
          Base.SelectRecordByXRef(TGEDCOMRepositoryRecord(cit.Value).XRef);
          Close;
        end;
      end;
    end;
  end;
end;

function TfmSourceEdit.GetBase: TfmBase;
begin
  Result := TfmBase(Owner);
end;

procedure TfmSourceEdit.EditShortTitleChange(Sender: TObject);
begin
  Caption := '�������� "'+EditShortTitle.Text+'"';
end;

end.
