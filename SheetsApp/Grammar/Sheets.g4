grammar Sheets;

expression
    : expression '^' expression                # PowerExpr
    | expression '*' expression                # MultiplyExpr
    | expression '/' expression                # DivideExpr
    | expression '+' expression                # AddExpr
    | expression '-' expression                # SubtractExpr
    | '--' expression                          # DecrementExpr
    | '++' expression                          # IncrementExpr
    | '-' expression                           # NegateExpr
    | '(' expression ')'                       # ParenExpr
    | cell                                     # CellExpr
    | number                                   # NumberExpr
    ;

number      : DIGIT+ ('.' DIGIT+)?;
cell        : LETTER+ DIGIT+;
 
DIGIT       : [0-9] ;
LETTER      : [A-Z] ;

WS          : [ \t\r\n]+ -> skip ; // Ignore whitespace
