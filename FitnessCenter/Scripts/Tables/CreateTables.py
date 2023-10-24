import psycopg2
import json

with open("..\\FitnessCenter\\Scripts\\Settings.json") as file:
    settings = json.load(file)

connection = psycopg2.connect(
    database=settings["database"],
    host=settings["host"],
    password=settings["password"],
    user=settings["user"]
)

cursor = connection.cursor()

script_files = [
    "..\\FitnessCenter\\Tables\\DayOfTheWeek.sql",
    "..\\FitnessCenter\\Tables\\Time.sql",
    "..\\FitnessCenter\\Tables\\TimetableOfClasses.sql",
    "..\\FitnessCenter\\Tables\\TypeOfOccupation.sql",
    "..\\FitnessCenter\\Tables\\Group.sql",
    "..\\FitnessCenter\\Tables\\Subscription.sql",
    "..\\FitnessCenter\\Tables\\Trainer.sql",
    "..\\FitnessCenter\\Tables\\BodyMassIndex.sql",
    "..\\FitnessCenter\\Tables\\Client.sql",
    "..\\FitnessCenter\\Tables\\Visit.sql",
    "..\\FitnessCenter\\Tables\\ClientGroup.sql",
    "..\\FitnessCenter\\Tables\\ClientSubscription.sql"
]   

for script_file in script_files:
    with open(script_file, 'r') as file:
        script = file.read()
        cursor.execute(script)
        connection.commit()

cursor.close()

connection.close()
