import shutil
from art import text2art
from prompt_toolkit.shortcuts import clear

def show_welcome_message():
    clear()
    rows, columns = shutil.get_terminal_size()
    welcome_message = text2art("Bienvenido!")
    lines = welcome_message.split('\n')
    centered_message = '\n'.join(['{:^{}}'.format(line, columns) for line in lines])
    centered_welcome = '{:^{}}'.format("Bienvenido al editor de código terminal en Python!", columns)
    centered_instructions = '{:^{}}'.format("Escribe 'q:Folder ./path/folder' para abrir una carpeta o 'q:File ./path/file' para abrir un archivo.", columns)
    print(centered_message)
    print(centered_welcome)
    print(centered_instructions)