%define		summary GEDKeeper - program for work with personal genealogical database.

Name:		gedkeeper
Version:	3.2.1
Release:	1%{?dist}
Summary:	%{summary}
License:	GPLv3
Group:		Applications/Editors
Url:		https://github.com/serg-norseman/gedkeeper
BuildArch:	x86_64

Requires:	dotnet-runtime-6.0
#Requires:	sqlite

%install

# main install
mkdir -p %{buildroot}%{_libdir}/%{name}
cp -r bin \
	locales \
	plugins \
	samples \
	scripts %{buildroot}%{_libdir}/%{name}

# clean multi-arch builds
ls -la %{buildroot}%{_libdir}/%{name}/plugins/runtimes/
rm -rf %{buildroot}%{_libdir}/%{name}/plugins/runtimes
install -v -t %{buildroot}%{_libdir}/%{name}/plugins/runtimes/linux-x64/native/ -D plugins/runtimes/linux-x64/native/*
ls -la %{buildroot}%{_libdir}/%{name}/plugins/runtimes/

# create binary file
mkdir -p %{buildroot}%{_bindir}
ln -fs %{buildroot}%{_libdir}/%{name}/bin/GEDKeeper3 %{buildroot}%{_bindir}/%{name}

install deploy/application-x-%{name}.xml %{buildroot}%{_metainfodir}/%{name}.metainfo.xml
install deploy/rpm/%{name}.desktop %{buildroot}%{_datadir}/applications/%{name}.desktop
install deploy/%{name}.png %{buildroot}%{_datadir}/pixmaps/%{name}.png

cd %{buildroot}
chmod -Rf a+rX,u+w,g-w,o-w .
chmod -Rf a-x .
cd %{buildroot}%{_bindir}
chmod a+x .
ls -la
cd %{buildroot}

ls -la usr/
ls -la usr/bin/

%files
%license LICENSE
%{_bindir}/%{name}
%{_libdir}/%{name}
%{_metainfodir}/%{name}.metainfo.xml
%{_datadir}/applications/%{name}.desktop
%{_datadir}/pixmaps/%{name}.png

%description
%{summary}

%changelog
* Apr 28 2023 GEDKeeper - 3.2.1
- New upstream release
