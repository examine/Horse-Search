# Horse Search

This project allows users to search for horse racing startlists by horse name.

## 📦 Structure

- **horse-search-frontend/** – React + TypeScript frontend
- **HorseSearchApi/** – .NET 9 Web API backend
- **HorseSearchSolution.sln** – .NET solution file

## 🚀 Getting Started

### Backend (.NET)

```bash
cd HorseSearchApi
dotnet run

### Frontend (React)
cd horse-search-frontend
npm install
npm start

Make sure the backend runs on https://localhost:5000 and that the frontend has this in package.json:
"proxy": "http://localhost:5000"

🔍 Search Example
Enter a horse name (e.g. Just Like Heaven) and press Search. If found in any startlist within 7 days, results will appear.

🛠 Tech Stack
React + TypeScript

.NET 9 Web API

HtmlAgilityPack (for HTML scraping)
