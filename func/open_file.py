async def open_file(filename):
    try:
        with open(filename, 'r', encoding='utf-8') as file:
            lines = file.readlines()
            for idx, line in enumerate(lines, start=1):
                print(f"{idx}: {line}", end='')
        return lines
    except UnicodeDecodeError:
        with open(filename, 'r', encoding='latin-1') as file:
            lines = file.readlines()
            for idx, line in enumerate(lines, start=1):
                print(f"{idx}: {line}", end='')
        return lines