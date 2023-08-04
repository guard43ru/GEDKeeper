%define		summary GEDKeeper - program for work with personal genealogical database.

Name:		gedkeeper
Version:	%{gh_version}
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

# clean multi-arch builds (TODO: fix me)
ls -la %{buildroot}%{_libdir}/%{name}/plugins/runtimes/
rm -rf %{buildroot}%{_libdir}/%{name}/plugins/runtimes
install -t %{buildroot}%{_libdir}/%{name}/plugins/runtimes/linux-x64/native/ -D plugins/runtimes/linux-x64/native/*
ls -la %{buildroot}%{_libdir}/%{name}/plugins/runtimes/

# symlink to binary file
mkdir -p %{buildroot}%{_bindir}
ln -fs %{_libdir}/%{name}/bin/GEDKeeper3 %{buildroot}%{_bindir}/%{name}

#rpm --eval %{_metainfodir}		# not found ?! (TODO: check rpm version)
#install -D deploy/application-x-%{name}.xml %{buildroot}%{_metainfodir}/%{name}.metainfo.xml
install -D deploy/application-x-%{name}.xml %{buildroot}%{_datadir}/metainfo/%{name}.metainfo.xml
install -D deploy/rpm/%{name}.desktop %{buildroot}%{_datadir}/applications/%{name}.desktop
install -D deploy/%{name}.png %{buildroot}%{_datadir}/pixmaps/%{name}.png

cd %{buildroot}
chmod -cRf a-x,a+rX,u+w,g-w,o-w .
chmod -v a+x %{buildroot}%{_libdir}/%{name}/bin/GEDKeeper3

%files
%license LICENSE
%{_bindir}/%{name}
%{_libdir}/%{name}
%{_datadir}/metainfo/%{name}.metainfo.xml
%{_datadir}/applications/%{name}.desktop
%{_datadir}/pixmaps/%{name}.png

%description
%{summary}

%changelog
* Apr 28 2023 GEDKeeper - 3.2.1
- New upstream release
