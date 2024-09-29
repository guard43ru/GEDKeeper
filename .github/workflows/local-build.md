Use act (https://nektosact.com/) for run GitHub Actions localy

1. Install gh (https://cli.github.com/) `dnf install gh`
1. Install act `gh extension install https://github.com/nektos/gh-act`
1. Get git submodules `git submodule init && git submodule update`
1. Run `gh act --artifact-server-path /path/to/result`
