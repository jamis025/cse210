import random
import datetime
import os

class Journal:
    def __init__(self):
        self.entries = []

    def write_new_entry(self):
        entry_text = input("Enter your journal entry: ")
        timestamp = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        self.entries.append(f"[{timestamp}] {entry_text}")
        print("Entry added successfully.")

    def display_journal(self):
        if not self.entries:
            print("Journal is empty.")
        else:
            for entry in self.entries:
                print(entry)

    def save_to_file(self, filename):
        try:
            with open(filename, 'w') as f:
                for entry in self.entries:
                    f.write(entry + '\n')
            print(f"Journal saved to {filename}")
        except IOError as e:
            print(f"Error saving file: {e}")

    def load_from_file(self, filename):
        try:
            with open(filename, 'r') as f:
                self.entries = [line.strip() for line in f.readlines()]
            print(f"Journal loaded from {filename}")
        except FileNotFoundError:
            print(f"File not found: {filename}")
        except IOError as e:
            print(f"Error loading file: {e}")

def main():
    journal = Journal()
    while True:
        print("\nPlease select one of the following choices:")
        print("1. Write a new entry")
        print("2. Display the journal")
        print("3. Save the journal to a file")
        print("4. Load the journal from a file")
        print("5. Quit")

        choice = input("What would you like to do? ")

        if choice == '1':
            journal.write_new_entry()
        elif choice == '2':
            journal.display_journal()
        elif choice == '3':
            filename = input("Enter filename to save to (e.g., my_journal.txt): ")
            journal.save_to_file(filename if filename else "journal.txt")
        elif choice == '4':
            filename = input("Enter filename to load from (e.g., my_journal.txt): ")
            journal.load_from_file(filename if filename else "journal.txt")
        elif choice == '5':
            print("Goodbye!")
            break
        else:
            print("\nInvalid choice. Please enter a number between 1 and 5.")

if __name__ == "__main__":
    main()