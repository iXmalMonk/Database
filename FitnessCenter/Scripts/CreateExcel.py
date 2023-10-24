import csv
from datetime import datetime
import json
from openpyxl import Workbook
import psycopg2

with open("..\\FitnessCenter\\Scripts\\Settings.json") as file:
    settings = json.load(file)

connection = psycopg2.connect(
    database=settings["database"],
    host=settings["host"],
    password=settings["password"],
    user=settings["user"]
)

cursor = connection.cursor()

tables = [
    "fc.body_mass_index",
    "fc.client",
    "fc.client_group",
    "fc.client_subscription",
    "fc.day_of_the_week",
    "fc.group",
    "fc.subscription",
    "fc.time",
    "fc.timetable_of_classes",
    "fc.trainer",
    "fc.type_of_occupation",
    "fc.visit"
]

for table in tables:
    query = "select * from " + table
    cursor.execute(query)
    column_names = [description[0] for description in cursor.description]
    result = cursor.fetchall()
    workbook = Workbook()
    sheet = workbook.active
    sheet.append(column_names)
    for row in result:
        sheet.append(row)
    workbook.save("..\\FitnessCenter\\Excel\\" + table + "_" + datetime.now().strftime("%Y_%m_%d_%H_%M_%S") + ".xlsx")
    with open("..\\FitnessCenter\\Excel\\Csv\\" + table + "_" + datetime.now().strftime("%Y_%m_%d_%H_%M_%S") + ".csv", "w", encoding="utf-8", newline="") as file:
        writer = csv.writer(file)
        writer.writerow(column_names)
        writer.writerows(result)

cursor.close()

connection.close()