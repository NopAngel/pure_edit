from pygments.lexers import PythonLexer, HtmlLexer, JavascriptLexer, JsonLexer, CLexer, RustLexer, CssLexer, BashLexer


def get_lexer_for_filename(filename):
    if filename.endswith('.py'):
        return PygmentsLexer(PythonLexer)
    elif filename.endswith('.html'):
        return PygmentsLexer(HtmlLexer)
    elif filename.endswith('.js'):
        return PygmentsLexer(JavascriptLexer)
    elif filename.endswith('.json'):
        return PygmentsLexer(JsonLexer)
    elif filename.endswith('.bash') or filename.endswith(".sh"):
        return PygmentsLexer(BashLexer)
    elif filename.endswith('.css'):
        return PygmentsLexer(CssLexer)
    elif filename.endswith('.c'):
        return PygmentsLexer(CLexer)
    elif filename.endswith('.rs'):
        return PygmentsLexer(RustLexer)
    else:
        return None