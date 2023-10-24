import json
from datetime import datetime
import subprocess

with open("..\\FitnessCenter\\Scripts\\Settings.json") as file:
    settings = json.load(file)

dump_file = "..\\FitnessCenter\\Dump\\" + "dump_" + datetime.now().strftime("%Y_%m_%d_%H_%M_%S") + ".dump"

command = f'pg_dump --dbname="{settings["database"]}" --host="{settings["host"]}" --username="{settings["user"]}" --format=custom --file="{dump_file}"'

subprocess.run(command, shell=True)
