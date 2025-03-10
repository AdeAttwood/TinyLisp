grammar TinyLisp;

file : list* EOF ;

list : LeftPeren listItems* RightPeren ;

quotedList : Quote LeftPeren listItems* RightPeren ;

listItems
    : BooleanConstant
    | Number
    | ID
    | DoubleQuoteString
    | quotedList
    | list
    ;

BooleanConstant   : 'true' | 'false';
Number            : ( [0-9]* '.' )? [0-9]+;
ID                : ( [A-Za-z0-9] | '-' | '+' | '#' | '/' | '!' | '?' | '=' | '*' | '<' | '>' )+;
DoubleQuoteString : '"' (~('"' | '\\') | '\\' . )* '"';
Quote             : '\'';
LeftPeren         : '(';
RightPeren        : ')';

Comment:  ';;' ~[\r\n]* -> skip;
SPACE: [ \t\r\n]+ -> skip;

