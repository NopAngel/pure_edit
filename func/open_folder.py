import os
async def open_folder(path):
    try:
        files = os.listdir(path)
        for idx, file in enumerate(files, start=1):
            print(f"{idx}: {file}")
        return files
    except Exception as e:
        print(f"Error al abrir la carpeta: {e}")
        return []