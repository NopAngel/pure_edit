import os

def check_terminal_version():
    if 'TERM_PROGRAM' in os.environ:
        if os.environ['TERM_PROGRAM'] == 'Apple_Terminal':
            return 'compatible'
    if 'TERM' in os.environ:
        if os.environ['TERM'] in ['xterm-256color', 'screen-256color']:
            return 'compatible'
    return 'no_compatible'