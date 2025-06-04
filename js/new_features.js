// ===== THEME TOGGLE =====
let darkMode = true;

function toggleTheme() {
    darkMode = !darkMode;
    document.body.classList.toggle('light-mode', !darkMode);
    document.querySelector('.container').classList.toggle('light-mode', !darkMode);
    document.getElementById('themeToggle').innerText = darkMode ? '🌙 Dark Mode' : '☀️ Light Mode';
}

// ===== MOVIE MODAL =====
let currentMovieId = null;

function openModal(movie) {
    currentMovieId = movie.id;
    document.getElementById('modalPoster').src = "https://image.tmdb.org/t/p/w500" + movie.poster_path;
    document.getElementById('modalTitle').innerText = movie.title;
    document.getElementById('modalRating').innerText = movie.vote_average.toFixed(1) + " / 10";
    document.getElementById('modalYear').innerText = movie.release_date.split('-')[0];
    document.getElementById('modalOverview').innerText = movie.overview || "No overview available.";
    loadUserRating(movie.id);
    loadComments(movie.id);
    document.getElementById('movieModal').style.display = 'block';
}

function closeModal() {
    document.getElementById('movieModal').style.display = 'none';
    currentMovieId = null;
}

// ===== FAVORITES =====
function saveFavorite(movie) {
    let favorites = JSON.parse(localStorage.getItem('favorites')) || [];
    if (!favorites.some(m => m.id === movie.id)) {
        favorites.push(movie);
        localStorage.setItem('favorites', JSON.stringify(favorites));
        displayFavorites();
    }
}

function displayFavorites() {
    const list = document.getElementById('favoritesList');
    list.innerHTML = '';
    const favorites = JSON.parse(localStorage.getItem('favorites')) || [];
    favorites.forEach(movie => {
        const item = document.createElement('li');
        item.className = 'recommendation-item';
        item.innerHTML = `
            <img src="https://image.tmdb.org/t/p/w500${movie.poster_path}"  alt="${movie.title}">
            <h4>${movie.title}</h4>
            <p>⭐ ${movie.vote_average.toFixed(1)}</p>
        `;
        item.onclick = () => openModal(movie);
        list.appendChild(item);
    });
}

displayFavorites();

// ===== SEARCH MOVIES =====
function searchMovies() {
    const query = document.getElementById('searchInput').value.toLowerCase();
    const items = document.querySelectorAll('.recommendation-item');
    items.forEach(item => {
        const title = item.querySelector('h4').innerText.toLowerCase();
        item.style.display = title.includes(query) ? 'block' : 'none';
    });
}

// ===== USER RATING =====
function createRatingStars(container, movieId) {
    container.innerHTML = '';
    for (let i = 1; i <= 5; i++) {
        const star = document.createElement('span');
        star.className = 'rating-star';
        star.innerText = '★';
        star.dataset.value = i;
        star.onclick = () => rateMovie(movieId, i);
        container.appendChild(star);
    }
    updateRatingDisplay(container, movieId);
}

function updateRatingDisplay(container, movieId) {
    const ratings = JSON.parse(localStorage.getItem('ratings') || '{}');
    const stars = container.querySelectorAll('.rating-star');
    stars.forEach(star => {
        star.classList.remove('active');
        if (+star.dataset.value <= Math.round(ratings[movieId] || 0)) {
            star.classList.add('active');
        }
    });
}

function rateMovie(movieId, rating) {
    const ratings = JSON.parse(localStorage.getItem('ratings') || '{}');
    ratings[movieId] = rating;
    localStorage.setItem('ratings', JSON.stringify(ratings));
    updateRatingDisplay(document.getElementById('ratingStars'), movieId);
}

function loadUserRating(movieId) {
    const ratings = JSON.parse(localStorage.getItem('ratings') || '{}');
    const rating = ratings[movieId] || 0;
    const container = document.getElementById('ratingStars');
    createRatingStars(container, movieId);
}

// ===== COMMENTS =====
function addComment() {
    const input = document.getElementById('commentInput');
    const text = input.value.trim();
    if (!text || !currentMovieId) return;
    const comments = JSON.parse(localStorage.getItem('comments') || '{}');
    if (!comments[currentMovieId]) comments[currentMovieId] = [];
    comments[currentMovieId].push(text);
    localStorage.setItem('comments', JSON.stringify(comments));
    input.value = '';
    loadComments(currentMovieId);
}

function loadComments(movieId) {
    const list = document.getElementById('commentList');
    list.innerHTML = '';
    const comments = JSON.parse(localStorage.getItem('comments') || '{}')[movieId] || [];
    comments.forEach(comment => {
        const li = document.createElement('li');
        li.innerText = comment;
        list.appendChild(li);
    });
}