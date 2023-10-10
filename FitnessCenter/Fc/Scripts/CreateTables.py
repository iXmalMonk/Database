import psycopg2
import json

with open("..\\FitnessCenter\\Fc\\Scripts\\Settings.json") as file:
    settings = json.load(file)

connection = psycopg2.connect(
    host=settings["host"],
    database=settings["database"],
    user=settings["user"],
    password=settings["password"]
)

cursor = connection.cursor()

script_files = [
    "..\\FitnessCenter\\Fc\\Tables\\DayOfTheWeek.sql",
    "..\\FitnessCenter\\Fc\\Tables\\Time.sql",
    "..\\FitnessCenter\\Fc\\Tables\\TimetableOfClasses.sql",
    "..\\FitnessCenter\\Fc\\Tables\\TypeOfOccupation.sql",
    "..\\FitnessCenter\\Fc\\Tables\\Group.sql",
    "..\\FitnessCenter\\Fc\\Tables\\Subscription.sql",
    "..\\FitnessCenter\\Fc\\Tables\\Trainer.sql",
    "..\\FitnessCenter\\Fc\\Tables\\BodyMassIndex.sql",
    "..\\FitnessCenter\\Fc\\Tables\\Client.sql",
    "..\\FitnessCenter\\Fc\\Tables\\Visit.sql",
    "..\\FitnessCenter\\Fc\\Tables\\ClientGroup.sql",
    "..\\FitnessCenter\\Fc\\Tables\\ClientSubscription.sql"
]   

for script_file in script_files:
    with open(script_file, 'r') as file:
        script = file.read()
        cursor.execute(script)
        connection.commit()

cursor.close()

connection.close()
