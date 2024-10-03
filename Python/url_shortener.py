import requests

def shorten_url(long_url):
    api_url = "https://api.tinyurl.com/create"
    params = {
        "url": long_url
    }
    
    response = requests.post(api_url, json=params)
    
    if response.status_code == 200:
        return response.json().get("tinyurl")
    else:
        return None

def main():
    print("Welcome to the URL Shortener!")
    long_url = input("Enter the URL you want to shorten: ")
    
    short_url = shorten_url(long_url)
    
    if short_url:
        print(f"Shortened URL: {short_url}")
    else:
        print("Error: Unable to shorten the URL.")

if __name__ == "__main__":
    main()
