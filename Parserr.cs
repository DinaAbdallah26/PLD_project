
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF            =  0, // (EOF)
        SYMBOL_ERROR          =  1, // (Error)
        SYMBOL_COMMENT        =  2, // Comment
        SYMBOL_WHITESPACE     =  3, // Whitespace
        SYMBOL_MINUS          =  4, // '-'
        SYMBOL_EXCLAMEQ       =  5, // '!='
        SYMBOL_PERCENT        =  6, // '%'
        SYMBOL_LPAREN         =  7, // '('
        SYMBOL_RPAREN         =  8, // ')'
        SYMBOL_TIMES          =  9, // '*'
        SYMBOL_COMMA          = 10, // ','
        SYMBOL_DIV            = 11, // '/'
        SYMBOL_COLON          = 12, // ':'
        SYMBOL_LBRACKET       = 13, // '['
        SYMBOL_RBRACKET       = 14, // ']'
        SYMBOL_PLUS           = 15, // '+'
        SYMBOL_LT             = 16, // '<'
        SYMBOL_LTEQ           = 17, // '<='
        SYMBOL_EQ             = 18, // '='
        SYMBOL_EQEQ           = 19, // '=='
        SYMBOL_GT             = 20, // '>'
        SYMBOL_GTEQ           = 21, // '>='
        SYMBOL_AND            = 22, // and
        SYMBOL_DEF            = 23, // def
        SYMBOL_ELSE           = 24, // else
        SYMBOL_FALSE          = 25, // False
        SYMBOL_FLOATLITERAL   = 26, // FloatLiteral
        SYMBOL_FOR            = 27, // for
        SYMBOL_IDENTIFIER     = 28, // Identifier
        SYMBOL_IF             = 29, // if
        SYMBOL_IN             = 30, // in
        SYMBOL_INTLITERAL     = 31, // IntLiteral
        SYMBOL_NONE           = 32, // None
        SYMBOL_NOT            = 33, // not
        SYMBOL_OR             = 34, // or
        SYMBOL_PRINT          = 35, // print
        SYMBOL_RETURN         = 36, // return
        SYMBOL_STRINGLITERAL  = 37, // StringLiteral
        SYMBOL_TRUE           = 38, // True
        SYMBOL_WHILE          = 39, // while
        SYMBOL_ADDEXPR        = 40, // <AddExpr>
        SYMBOL_ANDEXPR        = 41, // <AndExpr>
        SYMBOL_ARGUMENTLIST   = 42, // <ArgumentList>
        SYMBOL_ASSIGNSTMT     = 43, // <AssignStmt>
        SYMBOL_BLOCK          = 44, // <Block>
        SYMBOL_COMPEXPR       = 45, // <CompExpr>
        SYMBOL_EXPRESSION     = 46, // <Expression>
        SYMBOL_EXPRESSIONSTMT = 47, // <ExpressionStmt>
        SYMBOL_FORSTMT        = 48, // <ForStmt>
        SYMBOL_FUNCTIONCALL   = 49, // <FunctionCall>
        SYMBOL_FUNCTIONDEF    = 50, // <FunctionDef>
        SYMBOL_IFSTMT         = 51, // <IfStmt>
        SYMBOL_LISTEXPR       = 52, // <ListExpr>
        SYMBOL_MULTEXPR       = 53, // <MultExpr>
        SYMBOL_NOTEXPR        = 54, // <NotExpr>
        SYMBOL_OREXPR         = 55, // <OrExpr>
        SYMBOL_PARAMETERLIST  = 56, // <ParameterList>
        SYMBOL_PRIMARYEXPR    = 57, // <PrimaryExpr>
        SYMBOL_PRINTSTMT      = 58, // <PrintStmt>
        SYMBOL_PROGRAM        = 59, // <Program>
        SYMBOL_RETURNSTMT     = 60, // <ReturnStmt>
        SYMBOL_STATEMENT      = 61, // <Statement>
        SYMBOL_STATEMENTLIST  = 62, // <StatementList>
        SYMBOL_UNARYEXPR      = 63, // <UnaryExpr>
        SYMBOL_WHILESTMT      = 64  // <WhileStmt>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM                                         =  0, // <Program> ::= <StatementList>
        RULE_STATEMENTLIST                                   =  1, // <StatementList> ::= <Statement>
        RULE_STATEMENTLIST2                                  =  2, // <StatementList> ::= <StatementList> <Statement>
        RULE_STATEMENT                                       =  3, // <Statement> ::= <AssignStmt>
        RULE_STATEMENT2                                      =  4, // <Statement> ::= <PrintStmt>
        RULE_STATEMENT3                                      =  5, // <Statement> ::= <IfStmt>
        RULE_STATEMENT4                                      =  6, // <Statement> ::= <WhileStmt>
        RULE_STATEMENT5                                      =  7, // <Statement> ::= <ForStmt>
        RULE_STATEMENT6                                      =  8, // <Statement> ::= <FunctionDef>
        RULE_STATEMENT7                                      =  9, // <Statement> ::= <ReturnStmt>
        RULE_STATEMENT8                                      = 10, // <Statement> ::= <ExpressionStmt>
        RULE_ASSIGNSTMT_IDENTIFIER_EQ                        = 11, // <AssignStmt> ::= Identifier '=' <Expression>
        RULE_PRINTSTMT_PRINT_LPAREN_RPAREN                   = 12, // <PrintStmt> ::= print '(' <Expression> ')'
        RULE_IFSTMT_IF_COLON                                 = 13, // <IfStmt> ::= if <Expression> ':' <Block>
        RULE_IFSTMT_IF_COLON_ELSE_COLON                      = 14, // <IfStmt> ::= if <Expression> ':' <Block> else ':' <Block>
        RULE_WHILESTMT_WHILE_COLON                           = 15, // <WhileStmt> ::= while <Expression> ':' <Block>
        RULE_FORSTMT_FOR_IDENTIFIER_IN_COLON                 = 16, // <ForStmt> ::= for Identifier in <Expression> ':' <Block>
        RULE_FUNCTIONDEF_DEF_IDENTIFIER_LPAREN_RPAREN_COLON  = 17, // <FunctionDef> ::= def Identifier '(' <ParameterList> ')' ':' <Block>
        RULE_FUNCTIONDEF_DEF_IDENTIFIER_LPAREN_RPAREN_COLON2 = 18, // <FunctionDef> ::= def Identifier '(' ')' ':' <Block>
        RULE_RETURNSTMT_RETURN                               = 19, // <ReturnStmt> ::= return <Expression>
        RULE_RETURNSTMT_RETURN2                              = 20, // <ReturnStmt> ::= return
        RULE_EXPRESSIONSTMT                                  = 21, // <ExpressionStmt> ::= <Expression>
        RULE_BLOCK                                           = 22, // <Block> ::= <StatementList>
        RULE_PARAMETERLIST_IDENTIFIER                        = 23, // <ParameterList> ::= Identifier
        RULE_PARAMETERLIST_COMMA_IDENTIFIER                  = 24, // <ParameterList> ::= <ParameterList> ',' Identifier
        RULE_EXPRESSION                                      = 25, // <Expression> ::= <OrExpr>
        RULE_OREXPR                                          = 26, // <OrExpr> ::= <AndExpr>
        RULE_OREXPR_OR                                       = 27, // <OrExpr> ::= <OrExpr> or <AndExpr>
        RULE_ANDEXPR                                         = 28, // <AndExpr> ::= <NotExpr>
        RULE_ANDEXPR_AND                                     = 29, // <AndExpr> ::= <AndExpr> and <NotExpr>
        RULE_NOTEXPR                                         = 30, // <NotExpr> ::= <CompExpr>
        RULE_NOTEXPR_NOT                                     = 31, // <NotExpr> ::= not <NotExpr>
        RULE_COMPEXPR                                        = 32, // <CompExpr> ::= <AddExpr>
        RULE_COMPEXPR_EQEQ                                   = 33, // <CompExpr> ::= <CompExpr> '==' <AddExpr>
        RULE_COMPEXPR_EXCLAMEQ                               = 34, // <CompExpr> ::= <CompExpr> '!=' <AddExpr>
        RULE_COMPEXPR_LT                                     = 35, // <CompExpr> ::= <CompExpr> '<' <AddExpr>
        RULE_COMPEXPR_GT                                     = 36, // <CompExpr> ::= <CompExpr> '>' <AddExpr>
        RULE_COMPEXPR_LTEQ                                   = 37, // <CompExpr> ::= <CompExpr> '<=' <AddExpr>
        RULE_COMPEXPR_GTEQ                                   = 38, // <CompExpr> ::= <CompExpr> '>=' <AddExpr>
        RULE_ADDEXPR                                         = 39, // <AddExpr> ::= <MultExpr>
        RULE_ADDEXPR_PLUS                                    = 40, // <AddExpr> ::= <AddExpr> '+' <MultExpr>
        RULE_ADDEXPR_MINUS                                   = 41, // <AddExpr> ::= <AddExpr> '-' <MultExpr>
        RULE_MULTEXPR                                        = 42, // <MultExpr> ::= <UnaryExpr>
        RULE_MULTEXPR_TIMES                                  = 43, // <MultExpr> ::= <MultExpr> '*' <UnaryExpr>
        RULE_MULTEXPR_DIV                                    = 44, // <MultExpr> ::= <MultExpr> '/' <UnaryExpr>
        RULE_MULTEXPR_PERCENT                                = 45, // <MultExpr> ::= <MultExpr> '%' <UnaryExpr>
        RULE_UNARYEXPR                                       = 46, // <UnaryExpr> ::= <PrimaryExpr>
        RULE_UNARYEXPR_MINUS                                 = 47, // <UnaryExpr> ::= '-' <UnaryExpr>
        RULE_UNARYEXPR_PLUS                                  = 48, // <UnaryExpr> ::= '+' <UnaryExpr>
        RULE_PRIMARYEXPR_IDENTIFIER                          = 49, // <PrimaryExpr> ::= Identifier
        RULE_PRIMARYEXPR_INTLITERAL                          = 50, // <PrimaryExpr> ::= IntLiteral
        RULE_PRIMARYEXPR_FLOATLITERAL                        = 51, // <PrimaryExpr> ::= FloatLiteral
        RULE_PRIMARYEXPR_STRINGLITERAL                       = 52, // <PrimaryExpr> ::= StringLiteral
        RULE_PRIMARYEXPR_TRUE                                = 53, // <PrimaryExpr> ::= True
        RULE_PRIMARYEXPR_FALSE                               = 54, // <PrimaryExpr> ::= False
        RULE_PRIMARYEXPR_NONE                                = 55, // <PrimaryExpr> ::= None
        RULE_PRIMARYEXPR_LPAREN_RPAREN                       = 56, // <PrimaryExpr> ::= '(' <Expression> ')'
        RULE_PRIMARYEXPR                                     = 57, // <PrimaryExpr> ::= <FunctionCall>
        RULE_PRIMARYEXPR2                                    = 58, // <PrimaryExpr> ::= <ListExpr>
        RULE_FUNCTIONCALL_IDENTIFIER_LPAREN_RPAREN           = 59, // <FunctionCall> ::= Identifier '(' <ArgumentList> ')'
        RULE_FUNCTIONCALL_IDENTIFIER_LPAREN_RPAREN2          = 60, // <FunctionCall> ::= Identifier '(' ')'
        RULE_ARGUMENTLIST                                    = 61, // <ArgumentList> ::= <Expression>
        RULE_ARGUMENTLIST_COMMA                              = 62, // <ArgumentList> ::= <ArgumentList> ',' <Expression>
        RULE_LISTEXPR_LBRACKET_RBRACKET                      = 63, // <ListExpr> ::= '[' <ArgumentList> ']'
        RULE_LISTEXPR_LBRACKET_RBRACKET2                     = 64  // <ListExpr> ::= '[' ']'
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;
        ListBox ls;
        public MyParser(string filename , ListBox lst , ListBox ls)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this .lst = lst;
            this .ls = ls;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMENT :
                //Comment
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACKET :
                //'['
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACKET :
                //']'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AND :
                //and
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEF :
                //def
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FALSE :
                //False
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOATLITERAL :
                //FloatLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IN :
                //in
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTLITERAL :
                //IntLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NONE :
                //None
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NOT :
                //not
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OR :
                //or
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRINT :
                //print
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RETURN :
                //return
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL :
                //StringLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TRUE :
                //True
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADDEXPR :
                //<AddExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ANDEXPR :
                //<AndExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARGUMENTLIST :
                //<ArgumentList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNSTMT :
                //<AssignStmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BLOCK :
                //<Block>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMPEXPR :
                //<CompExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSIONSTMT :
                //<ExpressionStmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORSTMT :
                //<ForStmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTIONCALL :
                //<FunctionCall>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTIONDEF :
                //<FunctionDef>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFSTMT :
                //<IfStmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LISTEXPR :
                //<ListExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MULTEXPR :
                //<MultExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NOTEXPR :
                //<NotExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OREXPR :
                //<OrExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMETERLIST :
                //<ParameterList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRIMARYEXPR :
                //<PrimaryExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRINTSTMT :
                //<PrintStmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<Program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RETURNSTMT :
                //<ReturnStmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<Statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTLIST :
                //<StatementList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_UNARYEXPR :
                //<UnaryExpr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILESTMT :
                //<WhileStmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM :
                //<Program> ::= <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTLIST :
                //<StatementList> ::= <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTLIST2 :
                //<StatementList> ::= <StatementList> <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<Statement> ::= <AssignStmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<Statement> ::= <PrintStmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT3 :
                //<Statement> ::= <IfStmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT4 :
                //<Statement> ::= <WhileStmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT5 :
                //<Statement> ::= <ForStmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT6 :
                //<Statement> ::= <FunctionDef>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT7 :
                //<Statement> ::= <ReturnStmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT8 :
                //<Statement> ::= <ExpressionStmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNSTMT_IDENTIFIER_EQ :
                //<AssignStmt> ::= Identifier '=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRINTSTMT_PRINT_LPAREN_RPAREN :
                //<PrintStmt> ::= print '(' <Expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTMT_IF_COLON :
                //<IfStmt> ::= if <Expression> ':' <Block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTMT_IF_COLON_ELSE_COLON :
                //<IfStmt> ::= if <Expression> ':' <Block> else ':' <Block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILESTMT_WHILE_COLON :
                //<WhileStmt> ::= while <Expression> ':' <Block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORSTMT_FOR_IDENTIFIER_IN_COLON :
                //<ForStmt> ::= for Identifier in <Expression> ':' <Block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONDEF_DEF_IDENTIFIER_LPAREN_RPAREN_COLON :
                //<FunctionDef> ::= def Identifier '(' <ParameterList> ')' ':' <Block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONDEF_DEF_IDENTIFIER_LPAREN_RPAREN_COLON2 :
                //<FunctionDef> ::= def Identifier '(' ')' ':' <Block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURNSTMT_RETURN :
                //<ReturnStmt> ::= return <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURNSTMT_RETURN2 :
                //<ReturnStmt> ::= return
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSIONSTMT :
                //<ExpressionStmt> ::= <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BLOCK :
                //<Block> ::= <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMETERLIST_IDENTIFIER :
                //<ParameterList> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMETERLIST_COMMA_IDENTIFIER :
                //<ParameterList> ::= <ParameterList> ',' Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <OrExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OREXPR :
                //<OrExpr> ::= <AndExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OREXPR_OR :
                //<OrExpr> ::= <OrExpr> or <AndExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ANDEXPR :
                //<AndExpr> ::= <NotExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ANDEXPR_AND :
                //<AndExpr> ::= <AndExpr> and <NotExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NOTEXPR :
                //<NotExpr> ::= <CompExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NOTEXPR_NOT :
                //<NotExpr> ::= not <NotExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPEXPR :
                //<CompExpr> ::= <AddExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPEXPR_EQEQ :
                //<CompExpr> ::= <CompExpr> '==' <AddExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPEXPR_EXCLAMEQ :
                //<CompExpr> ::= <CompExpr> '!=' <AddExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPEXPR_LT :
                //<CompExpr> ::= <CompExpr> '<' <AddExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPEXPR_GT :
                //<CompExpr> ::= <CompExpr> '>' <AddExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPEXPR_LTEQ :
                //<CompExpr> ::= <CompExpr> '<=' <AddExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPEXPR_GTEQ :
                //<CompExpr> ::= <CompExpr> '>=' <AddExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXPR :
                //<AddExpr> ::= <MultExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXPR_PLUS :
                //<AddExpr> ::= <AddExpr> '+' <MultExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXPR_MINUS :
                //<AddExpr> ::= <AddExpr> '-' <MultExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXPR :
                //<MultExpr> ::= <UnaryExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXPR_TIMES :
                //<MultExpr> ::= <MultExpr> '*' <UnaryExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXPR_DIV :
                //<MultExpr> ::= <MultExpr> '/' <UnaryExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXPR_PERCENT :
                //<MultExpr> ::= <MultExpr> '%' <UnaryExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPR :
                //<UnaryExpr> ::= <PrimaryExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPR_MINUS :
                //<UnaryExpr> ::= '-' <UnaryExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UNARYEXPR_PLUS :
                //<UnaryExpr> ::= '+' <UnaryExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXPR_IDENTIFIER :
                //<PrimaryExpr> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXPR_INTLITERAL :
                //<PrimaryExpr> ::= IntLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXPR_FLOATLITERAL :
                //<PrimaryExpr> ::= FloatLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXPR_STRINGLITERAL :
                //<PrimaryExpr> ::= StringLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXPR_TRUE :
                //<PrimaryExpr> ::= True
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXPR_FALSE :
                //<PrimaryExpr> ::= False
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXPR_NONE :
                //<PrimaryExpr> ::= None
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXPR_LPAREN_RPAREN :
                //<PrimaryExpr> ::= '(' <Expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXPR :
                //<PrimaryExpr> ::= <FunctionCall>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXPR2 :
                //<PrimaryExpr> ::= <ListExpr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONCALL_IDENTIFIER_LPAREN_RPAREN :
                //<FunctionCall> ::= Identifier '(' <ArgumentList> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTIONCALL_IDENTIFIER_LPAREN_RPAREN2 :
                //<FunctionCall> ::= Identifier '(' ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTLIST :
                //<ArgumentList> ::= <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTLIST_COMMA :
                //<ArgumentList> ::= <ArgumentList> ',' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LISTEXPR_LBRACKET_RBRACKET :
                //<ListExpr> ::= '[' <ArgumentList> ']'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LISTEXPR_LBRACKET_RBRACKET2 :
                //<ListExpr> ::= '[' ']'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+" in line: "+ args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2= "Expected token: "+args.ExpectedTokens.ToString();
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }
        private void TokenReadEvent(LALRParser pr, TokenReadEventArgs args)
        {
            string info = args.Token.Text + "    \t    \t   " + (SymbolConstants)args.Token.Symbol.Id;
            ls.Items.Add(info);

        }
    }
}
