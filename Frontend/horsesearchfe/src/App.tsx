import React, { useState } from "react";

interface SearchResult {
  date: string;
  track: string;
  url: string;
}

const App: React.FC = () => {
  const [horseName, setHorseName] = useState("");
  const [results, setResults] = useState<SearchResult[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleSearch = async () => {
    setLoading(true);
    setError("");
    setResults([]);

    try {
      // const response = await fetch(
      //   `https://horse-search-api.azurewebsites.net/api/search?horseName=${encodeURIComponent(horseName)}`
      // );

      const response = await fetch(
        `https://horse-search-api.azurewebsites.net/api/search?horseName=${encodeURIComponent(horseName)}`
      );

      if (!response.ok) {
        throw new Error("Search failed");
      }

      const data = await response.json();
      setResults(data);
    } catch (err) {
      setError("Failed to fetch search results.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ maxWidth: 600, margin: "0 auto", padding: "2rem" }}>
      <h1>Horse Startlist Search</h1>

      <input
        type="text"
        value={horseName}
        onChange={(e) => setHorseName(e.target.value)}
        placeholder="Enter horse name"
        style={{ width: "100%", padding: "0.5rem", marginBottom: "1rem" }}
      />
      <button
        onClick={handleSearch}
        disabled={loading || !horseName.trim()}
        style={{ padding: "0.5rem 1rem" }}
      >
        {loading ? "Searching..." : "Search"}
      </button>

      {error && <p style={{ color: "red" }}>{error}</p>}

      {results.length > 0 && (
        <div style={{ marginTop: "2rem" }}>
          <h2>Results:</h2>
          <ul style={{ listStyle: "none", padding: 0 }}>
            {results.map((result, index) => (
              <li
                key={index}
                style={{
                  marginBottom: "1rem",
                  padding: "1rem",
                  border: "1px solid #ccc",
                  borderRadius: "8px",
                }}
              >
                <strong>Date:</strong> {result.date} <br />
                <strong>Track:</strong> {result.track} <br />
                <a href={result.url} target="_blank" rel="noopener noreferrer">
                  View Startlist
                </a>
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
};

export default App;
