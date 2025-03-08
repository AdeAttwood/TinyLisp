
alias tl = ./TinyLisp.Cli/bin/Release/net9.0/linux-x64/publish/TinyLisp.Cli

export def 'build antlr' [] {
  cd ./TinyLisp.Parser
  antlr4 -Dlanguage=CSharp  -visitor -listener ./TinyLisp.g4
}

export def 'build cli' [] {
  dotnet publish TinyLisp.Cli -c Release -r linux-x64 --self-contained false
}

export def build [] {
  build cli
}

export def test [] {
  tl test ./TinyLisp.Tests
}

export def format [] {
  dotnet format
}

export def pre-commit [] { format; build; test }
