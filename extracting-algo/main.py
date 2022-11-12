# imports
import pandas as pd
import json
import os
import time
import argparse
parser = argparse.ArgumentParser(description='A converter')
parser.add_argument("--html")
parser.add_argument("--xlsx")
parser.add_argument("--output")

args = parser.parse_args()
output = args.output
output = output + "\output"

# ansi escape sequence setup:
ansiEnd = "\033[0m"
os.system("") # enables ansi escape sequence
# Startup prints
print("Dieses Programm wird für Databases verwendet.",
      "\n" + "\033[43m \033[1m Bei erstmaliger Verwendung bitte README.pdf zu Rate ziehen!", ansiEnd, "\n", "\033[34m © POGnator, 2022", ansiEnd)
# Get the names and details of the html and Excel files
# try opening the file, else exit with error code 1
htmlFile = open(args.html)
#except:
#    input("\033[91m Datei nicht gefunden! Enter-Taste zum Beenden drücken." + ansiEnd)
#    htmlFile = None
#    time.sleep(.5)
#    exit(1)
#if not ".html" in str(htmlFile):
#    if input("\033[93m Achtung: In " + str(htmlFile.name) + " existiert keine .html-Erweiterung. Trotzdem fortfahren? (y/n) " + ansiEnd) == "n":
#        htmlFile = input("Name der HTML-Datei: ")
nameDB = str(args.xlsx)
#if not ".xlsx" in nameDB:
#    if input("\033[93m Achtung: In " + str(nameDB) + " existiert keine .xlsx-Erweiterung. Trotzdem fortfahren? (y/n) " + ansiEnd) == "n":
#        nameDB = input("Name der XLSX-Datei: ")
dBSheet = 0
if dBSheet is None:
    dBSheet = 0
try: # try opening the xlsx file plus its sheet, else exit with error code 1
    df = pd.read_excel(str(nameDB), sheet_name=dBSheet)
except:
    input("\033[91m Datei oder Sheet nicht gefunden! Enter-Taste zum Beenden drücken." + ansiEnd)
    df = None
    time.sleep(.5)
    exit(1)
# sets df to a python list instead of pandas format.
dataList = df.to_numpy().flatten()
htmRead = htmlFile.read()
# Debug section (makes you look like a hacker too lmao)
print("\033[49;3m")
print(dataList)
print(htmRead)
values = []
counter = 0
def write_json(new_data, filename=output + ".json"):
    with open(filename,'r+') as file:
        # Load existing data into a dict.
        file_data = json.load(file)
        # Join new_data with file_data inside emp_details
        file_data["'output'"].append(new_data)
        # Sets file's current position at offset.
        file.seek(0)
        # convert back to json.
        json.dump(file_data, file, indent = 4)
for i in dataList:
    if htmRead.count(i) != 0:
        if not os.path.exists(output + ".json"):
            with open(output + ".json", "a") as file:
                startForm = str({
                                repr("output"): [

                                ]
                            })
                file.write(startForm)
        # print(str(i) + ", " + str(htmRead.count(i)))
        values += [(str(i), str(htmRead.count(i)))]
        data = {"Name": str(i), "Count": str(htmRead.count(i))}
        write_json(data)
    counter += 1
# define list
# l = ['Hardik', 'Rohit', 'Rahul', 'Virat', 'Pant']
# replace first value
# l[0] = 'Shardul'
# converting output.json to excel format
with open (output + ".json", "r") as f:
    finalData = json.loads(f.read())
df_json = pd.json_normalize(finalData, record_path=["'output'"])
df_json.to_excel(output + ".xlsx", index=False)
datafr = pd.read_excel(output + ".xlsx")
datafr = datafr.sort_values("Count", ascending=False)
datafr.to_excel(output + ".xlsx", index=False)
print(datafr)
# Converted
print(values)
print(counter)
print(ansiEnd)
print("--------------------DEBUG END--------------------")
# Debug end
input("\033[42m Gespeichert als output.xlsx und output.json. Enter-Taste zum beenden drücken." + ansiEnd)
htmlFile.close()
