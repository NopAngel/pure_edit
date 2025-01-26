import jedi

def complete_code(text, cursor_pos):
    script = jedi.Script(text, 1, cursor_pos)
    completions = script.complete()
    return [completion.name for completion in completions]