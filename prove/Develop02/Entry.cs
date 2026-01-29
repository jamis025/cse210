class Entry:
    def __init__(self, prompt, response, date):
        self.prompt = prompt
        self.response = response
        self.date = date

    def display(self):
        """Displays the entry in a human-readable format."""
        print(f"Date: {self.date} - Prompt: {self.prompt}")
        print(f"Response: {self.response}\n")

    def serialize(self, separator='|'):
        """Serializes the entry into a string for file storage."""
        return f"{self.date}{separator}{self.prompt}{separator}{self.response}"

    @classmethod
    def deserialize(cls, data_string, separator='|'):
        """Creates an Entry object from a serialized string."""
        try:
            parts = data_string.strip().split(separator)
            if len(parts) == 3:
                return cls(parts[1], parts[2], parts[0])
            else:
                print(f"Skipping malformed line: {data_string}")
                return None
        except Exception as e:
            print(f"Error deserializing line: {e}")
            return None
   