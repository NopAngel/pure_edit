import os
import sys
import shutil
import asyncio
from prompt_toolkit import PromptSession, print_formatted_text, HTML
from prompt_toolkit.key_binding import KeyBindings
from prompt_toolkit.shortcuts import clear
from prompt_toolkit.styles import Style
from prompt_toolkit.lexers import PygmentsLexer
from art import text2art
from func import show_welcome_message
import jedi

async def main():
    font_path = os.path.abspath("fonts/font.ttf")

    show_welcome_message()
    
    session = PromptSession()
    bindings = KeyBindings()
    
    content = []
    current_line = 0
    current_file = None
    open_files = {}

    command = await session.prompt_async("Escriba su comando: ")
    if command.startswith("q:Folder "):
        path = command[len("q:Folder "):]
        files = await open_folder(path)
        while True:
            action = await session.prompt_async("Escribe 'new folder', 'new file', 'delete' o 'select' para continuar: ")
            if action == "new folder":
                folder_name = await session.prompt_async("Introduce el nombre de la nueva carpeta: ")
                os.makedirs(os.path.join(path, folder_name))
                files = await open_folder(path)
            elif action == "new file":
                file_name = await session.prompt_async("Introduce el nombre del nuevo archivo: ")
                with open(os.path.join(path, file_name), 'w') as f:
                    pass
                files = await open_folder(path)
            elif action == "delete":
                selected = await session.prompt_async("Introduce el número del archivo/carpeta a eliminar: ")
                try:
                    index = int(selected) - 1
                    os.remove(os.path.join(path, files[index]))
                    files = await open_folder(path)
                except (ValueError, IndexError):
                    print("Selección inválida.")
            elif action == "select":
                selected = await session.prompt_async("Introduce el número del archivo a seleccionar: ")
                try:
                    index = int(selected) - 1
                    selected_file = os.path.join(path, files[index])
                    if os.path.isfile(selected_file):
                        current_file = selected_file
                        content = await open_file(current_file)
                        break
                    else:
                        print("La selección no es un archivo válido.")
                except (ValueError, IndexError):
                    print("Selección inválida.")
    elif command.startswith("q:File "):
        filename = command[len("q:File "):]
        content = await open_file(filename)
        current_file = filename

    @bindings.add('escape')
    def exit_editor(event):
        if current_file:
            save_file(current_file, ''.join(content))
        clear()
        show_welcome_message()
        sys.exit(0)
    
    @bindings.add('c-s')
    def save(event):
        if current_file:
            save_file(current_file, ''.join(content))
    
    @bindings.add('c-l')
    async def command_prompt(event):
        command = await session.prompt_async("Comando (Ctrl+D para finalizar): ")
        os.system(command)
    
    @bindings.add('c-q')
    def switch_tabs(event):
        nonlocal current_file, content, current_line
        if open_files:
            files = list(open_files.keys())
            current_index = files.index(current_file)
            current_index = (current_index + 1) % len(files)
            current_file = files[current_index]
            content = open_files[current_file]
            current_line = len(content)
            clear()
            handle_navigation(content)

    @bindings.add('c-b')
    async def browse_files(event):
        nonlocal current_file, content, current_line
        path = await session.prompt_async("Introduce la ruta de la carpeta: ")
        files = await open_folder(path)
        while True:
            action = await session.prompt_async("Escribe 'new folder', 'new file', 'delete' o 'select' para continuar: ")
            if action == "new folder":
                folder_name = await session.prompt_async("Introduce el nombre de la nueva carpeta: ")
                os.makedirs(os.path.join(path, folder_name))
                files = await open_folder(path)
            elif action == "new file":
                file_name = await session.prompt_async("Introduce el nombre del nuevo archivo: ")
                with open(os.path.join(path, file_name), 'w') as f:
                    pass
                files = await open_folder(path)
            elif action == "delete":
                selected = await session.prompt_async("Introduce el número del archivo/carpeta a eliminar: ")
                try:
                    index = int(selected) - 1
                    os.remove(os.path.join(path, files[index]))
                    files = await open_folder(path)
                except (ValueError, IndexError):
                    print("Selección inválida.")
            elif action == "select":
                selected = await session.prompt_async("Introduce el número del archivo a seleccionar: ")
                try:
                    index = int(selected) - 1
                    selected_file = os.path.join(path, files[index])
                    if os.path.isfile(selected_file):
                        current_file = selected_file
                        content = await open_file(current_file)
                        break
                    else:
                        print("La selección no es un archivo válido.")
                except (ValueError, IndexError):
                    print("Selección inválida.")

    style = Style.from_dict({
        'prompt': 'bold #ebdbb2',
        'output': '#ebdbb2',
        'background': '#282828',
        'pygments.comment': 'italic #928374',
        'pygments.keyword': 'bold #fb4934',
        'pygments.string': '#b8bb26',
        'pygments.name': 'bold #83a598',
        'invalid': 'bg:#ff0000'
    })

    def handle_navigation(content):
        clear()
        for idx, line in enumerate(content, start=1):
            if not is_line_valid(line):
                print_formatted_text(HTML(f'<invalid>{idx}: {line}</invalid>'), style=style)
            else:
                print_formatted_text(f"{idx}: {line}", style=style)

    @bindings.add('up')
    def move_up(event):
        nonlocal current_line
        if current_line > 1:
            current_line -= 1
            handle_navigation(content)

    @bindings.add('down')
    def move_down(event):
        nonlocal current_line
        if current_line < len(content):
            current_line += 1
            handle_navigation(content)

    while True:
        try:
            lexer = get_lexer_for_filename(current_file) if current_file else None
            text = await session.prompt_async(f"{current_line + 1}: ", key_bindings=bindings, style=style, lexer=lexer, multiline=False, enable_history_search=True)
            if text == "":
                if current_line > 0 and current_line < len(content):
                    content.pop(current_line)
                elif current_line > 0:
                    current_line -= 1
                    content.pop(current_line)
                continue
            if current_line < len(content):
                content[current_line] = text + '\n'
            else:
                content.append(text + '\n')
            current_line += 1
        except EOFError:
            break
