import os

class DiskUsageAnalyzer:
    def __init__(self):
        # Initialize an empty dictionary to store file sizes
        self.file_sizes = {}

    def analyze_directory(self, directory):
        """
        Analyze the disk usage of a given directory.
        :param directory: The directory to analyze.
        """
        # Walk through the directory, including subdirectories
        for dirpath, _, filenames in os.walk(directory):
            for filename in filenames:
                # Construct the full file path
                filepath = os.path.join(dirpath, filename)
                try:
                    # Get the file size and store it in the dictionary
                    file_size = os.path.getsize(filepath)
                    self.file_sizes[filepath] = file_size
                except FileNotFoundError:
                    # Handle the case where the file is not found
                    print(f"File not found: {filepath}")
                except PermissionError:
                    # Handle permission errors
                    print(f"Permission denied: {filepath}")

    def get_largest_files(self, n=5):
        """
        Return the top N largest files in the analyzed directory.
        :param n: The number of largest files to return.
        :return: A list of tuples containing file paths and their sizes.
        """
        # Sort files by size in descending order and return the top N
        return sorted(self.file_sizes.items(), key=lambda x: -x[1])[:n]

# Test Cases
if __name__ == "__main__":
    # Create an instance of DiskUsageAnalyzer
    analyzer = DiskUsageAnalyzer()

    # Provide a directory to analyze, for example, 'test_folder'
    analyzer.analyze_directory("test_folder")

    # Display the top 5 largest files
    largest_files = analyzer.get_largest_files(5)
    print("Top 5 Largest Files:")
    for filepath, size in largest_files:
        # Convert size from bytes to MB for easier reading
        print(f"{filepath}: {size / (1024 * 1024):.2f} MB")
