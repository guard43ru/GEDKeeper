%define		rname GEDKeeper
%define		summary GEDKeeper - program for work with personal genealogical database

Name:		gedkeeper
Version:	3.2.1
Release:	1
Summary:	%{summary}
License:	GPLv3
Group:		Applications/Editors
Url:		https://github.com/serg-norseman/gedkeeper
BuildArch:	x86_64

%description
%{summary}.

Requires:	dotnet-runtime-6.0
Requires:	sqlite

AutoReq:	no
AutoReqProv:	no

%install
pwd
ls -la
#install -Dm 0755 gk_run.sh %{buildroot}%{_bindir}/gk_run.sh
#install -d 0755 %{buildroot}%{_libdir}/%{name}
#install -Dm 0644 application-x-%{name}.xml %{buildroot}%{_datadir}/mime/application-x-%{name}.xml
#install -Dm 0644 %{name}.desktop %{buildroot}%{_datadir}/applications/%{name}.desktop
#install -Dm 0644 %{name}.png %{buildroot}%{_datadir}/pixmaps/%{name}.png
#cp -r bin \
#	locales \
#	plugins \
#	samples \
#	scripts %{buildroot}%{_libdir}/%{name}
#/bin/chmod -Rf a+rX,u+w,g-w,o-w .
#/bin/chmodchmod -Rf a-x .

%files
%license LICENSE
%{_bindir}/gk_run.sh
%{_libdir}/%{name}
%{_datadir}/mime/*.xml
%{_datadir}/applications/%{name}.desktop
%{_datadir}/pixmaps/%{name}.png

%changelog
* Apr 28 2023 GEDKeeper - 3.2.1
- New upstream release
