class Entry:
    def __init__(self, prompt, response, date):
        self.prompt = prompt
        self.response = response
        self.date = date

    def display(self):
        print(f"Date: {self.date} - Prompt: {self.prompt}")
        print(f"Response: {self.response}\n")

    def serialize(self):
        return f"{self.date}|{self.prompt}|{self.response}"
