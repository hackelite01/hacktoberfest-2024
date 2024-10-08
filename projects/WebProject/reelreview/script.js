const apiKey = "c34d1e2c877bf6bc6dad87e5f9b087d5";
const baseUrl = "https://api.themoviedb.org/3";

// Elements
const searchInput = document.getElementById("searchMovie");
const movieList = document.getElementById("movie-list");
const modal = document.getElementById("review-modal");
const closeModal = document.querySelector(".close");
const submitReviewButton = document.getElementById("submit-review");
const reviewText = document.getElementById("review-text");
const movieTitle = document.getElementById("movie-title");

let currentMovieId = null; // To track which movie is being reviewed

// Open review modal
function openReviewModal(movieId, title) {
  modal.style.display = "flex";
  currentMovieId = movieId;
  movieTitle.textContent = title;
  reviewText.value = getReview(movieId) || ""; // Load saved review if it exists
}

// Close review modal
closeModal.onclick = function () {
  modal.style.display = "none";
};

// Close modal when clicking outside
window.onclick = function (event) {
  if (event.target === modal) {
    modal.style.display = "none";
  }
};

// Search for a movie when the user types
searchInput.addEventListener("input", () => {
  const query = searchInput.value;
  if (query) {
    searchMovies(query);
  }
});

// Fetch movies from TMDB API
async function searchMovies(query) {
  const url = `${baseUrl}/search/movie?api_key=${apiKey}&query=${encodeURIComponent(
    query
  )}`;
  const response = await fetch(url);
  const data = await response.json();
  displayMovies(data.results);
}

// Display movies on the page
function displayMovies(movies) {
  movieList.innerHTML = "";
  if (movies.length === 0) {
    movieList.innerHTML = "<p>No movies found</p>";
    return;
  }

  movies.forEach((movie) => {
    const movieDiv = document.createElement("div");
    movieDiv.classList.add("movie");

    const moviePoster = movie.poster_path
      ? `<img src="https://image.tmdb.org/t/p/w500${movie.poster_path}" alt="${movie.title}">`
      : '<div class="no-image">No Image</div>';

    const userReview = getReview(movie.id);

    movieDiv.innerHTML = `
            ${moviePoster}
            <h3>${movie.title}</h3>
            <p>Rating: ${movie.vote_average}</p>
            <p>${movie.release_date}</p>
            <p><strong>Your Review:</strong> ${
              userReview || "No review added yet"
            }</p>
            <button onclick="openReviewModal(${movie.id}, '${
      movie.title
    }')">Add/Edit Review</button>
        `;

    movieList.appendChild(movieDiv);
  });
}

// Add review button handler
submitReviewButton.onclick = function () {
  const review = reviewText.value.trim();
  if (currentMovieId && review) {
    saveReview(currentMovieId, review);
    modal.style.display = "none";
    searchMovies(searchInput.value); // Refresh movie list with new review
  }
};

// Save review to localStorage
function saveReview(movieId, review) {
  const reviews = JSON.parse(localStorage.getItem("movieReviews")) || {};
  reviews[movieId] = review;
  localStorage.setItem("movieReviews", JSON.stringify(reviews));
}

// Get review from localStorage
function getReview(movieId) {
  const reviews = JSON.parse(localStorage.getItem("movieReviews")) || {};
  return reviews[movieId];
}
