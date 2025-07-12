// ===== ENHANCED THEME TOGGLE =====
let currentMovieId = null;
let currentMovie = null;
let darkMode = localStorage.getItem('theme') === 'dark' || true;

function toggleDarkMode() {
    darkMode = !darkMode;
    document.body.classList.toggle('dark-mode', darkMode);
    localStorage.setItem('theme', darkMode ? 'dark' : 'light');
    document.getElementById('themeToggle').checked = darkMode;
    showNotification(darkMode ? 'Dark mode enabled' : 'Light mode enabled', 'info');
}

// Initialize theme on load
(function initializeTheme() {
    document.body.classList.toggle('dark-mode', darkMode);
    document.getElementById('themeToggle').checked = darkMode;
})();

// ===== ENHANCED MOVIE MODAL =====


function openModal(movie) {
    currentMovieId = movie.id;
    currentMovie = movie;

    // Set backdrop image
    const backdropUrl = movie.backdrop_path
        ? `https://image.tmdb.org/t/p/w1280${movie.backdrop_path}`
        : `https://image.tmdb.org/t/p/w500${movie.poster_path}`;

    document.getElementById('modalBackdrop').src = backdropUrl;
    document.getElementById('modalPoster').src = "https://image.tmdb.org/t/p/w500" + movie.poster_path;
    document.getElementById('modalTitle').innerText = movie.title;
    document.getElementById('modalRating').innerText = `⭐ ${movie.vote_average.toFixed(1)}/10`;
    document.getElementById('modalYear').innerText = movie.release_date.split('-')[0];
    document.getElementById('modalLanguage').innerText = movie.original_language.toUpperCase();
    document.getElementById('modalOverview').innerText = movie.overview || "No overview available.";

    // Update favorite button
    updateFavoriteButton();

    loadUserRating(movie.id);
    loadComments(movie.id);
    document.getElementById('movieModal').style.display = 'block';
    document.body.style.overflow = 'hidden';
}

function closeModal() {
    document.getElementById('movieModal').style.display = 'none';
    document.body.style.overflow = 'auto';
    currentMovieId = null;
    currentMovie = null;
}

// Close modal when clicking outside
document.getElementById('movieModal').addEventListener('click', function (e) {
    if (e.target === this) {
        closeModal();
    }
});

// ===== ENHANCED FAVORITES SYSTEM =====
function saveFavorite(movie) {
    let favorites = JSON.parse(localStorage.getItem('favorites')) || [];
    if (!favorites.some(m => m.id === movie.id)) {
        favorites.push(movie);
        localStorage.setItem('favorites', JSON.stringify(favorites));
        displayFavorites();
        updateStats();
        showNotification(`${movie.title} added to favorites!`, 'success');
        updateFavoriteButton();
    }
}

function removeFavorite(movieId) {
    let favorites = JSON.parse(localStorage.getItem('favorites')) || [];
    const movieTitle = favorites.find(m => m.id === movieId)?.title || 'Movie';
    favorites = favorites.filter(m => m.id !== movieId);
    localStorage.setItem('favorites', JSON.stringify(favorites));
    displayFavorites();
    updateStats();
    showNotification(`${movieTitle} removed from favorites`, 'info');
    updateFavoriteButton();
}

function toggleFavorite() {
    if (!currentMovie) return;

    const favorites = JSON.parse(localStorage.getItem('favorites')) || [];
    const isFavorite = favorites.some(m => m.id === currentMovie.id);

    if (isFavorite) {
        removeFavorite(currentMovie.id);
    } else {
        saveFavorite(currentMovie);
    }
}

function updateFavoriteButton() {
    if (!currentMovie) return;

    const favorites = JSON.parse(localStorage.getItem('favorites')) || [];
    const isFavorite = favorites.some(m => m.id === currentMovie.id);
    const btn = document.getElementById('favoriteBtn');

    if (isFavorite) {
        btn.innerHTML = '<span>💔</span> Remove from Favorites';
        btn.style.background = '#ef4444';
    } else {
        btn.innerHTML = '<span>❤️</span> Add to Favorites';
        btn.style.background = 'var(--gradient-accent)';
    }
}

function clearAllFavorites() {
    if (confirm('Are you sure you want to clear all favorites?')) {
        localStorage.removeItem('favorites');
        displayFavorites();
        updateStats();
        showNotification('All favorites cleared', 'info');
    }
}

function displayFavorites() {
    const list = document.getElementById('favoritesList');
    list.innerHTML = '';
    const favorites = JSON.parse(localStorage.getItem('favorites')) || [];

    // Update favorites count display
    document.getElementById('favoritesDisplayCount').innerText = favorites.length;

    if (favorites.length === 0) {
        list.innerHTML = '<li style="grid-column: 1/-1; text-align: center; padding: 3rem; opacity: 0.5;">No favorite movies yet. Start exploring!</li>';
        return;
    }

    favorites.forEach((movie, index) => {
        const item = document.createElement('li');
        item.className = 'recommendation-item';
        item.style.setProperty('--index', index);
        item.innerHTML = createMovieCard(movie, true);
        list.appendChild(item);
    });
}

// ===== ENHANCED SEARCH =====
function searchMovies() {
    const query = document.getElementById('searchInput').value.toLowerCase();
    const items = document.querySelectorAll('#recommendations .recommendation-item');
    let visibleCount = 0;

    items.forEach(item => {
        const title = item.querySelector('h4').innerText.toLowerCase();
        const overview = item.querySelector('.movie-overview').innerText.toLowerCase();
        const isVisible = title.includes(query) || overview.includes(query);

        item.style.display = isVisible ? 'block' : 'none';
        if (isVisible) visibleCount++;
    });

    // Update total movies count
    document.getElementById('totalMovies').innerText = visibleCount;
}

// ===== ENHANCED RATING SYSTEM =====
function createRatingStars(container, movieId) {
    container.innerHTML = '';
    for (let i = 1; i <= 5; i++) {
        const star = document.createElement('span');
        star.className = 'rating-star';
        star.innerText = '★';
        star.dataset.value = i;
        star.onclick = () => rateMovie(movieId, i);
        star.onmouseover = () => highlightStars(container, i);
        star.onmouseout = () => updateRatingDisplay(container, movieId);
        container.appendChild(star);
    }
    updateRatingDisplay(container, movieId);
}

function highlightStars(container, rating) {
    const stars = container.querySelectorAll('.rating-star');
    stars.forEach((star, index) => {
        star.style.color = index < rating ? '#fbbf24' : '#e5e7eb';
    });
}

function updateRatingDisplay(container, movieId) {
    const ratings = JSON.parse(localStorage.getItem('ratings') || '{}');
    const rating = ratings[movieId] || 0;
    const stars = container.querySelectorAll('.rating-star');

    stars.forEach((star, index) => {
        star.classList.toggle('active', index < rating);
    });

    // Update rating label
    const label = document.getElementById('ratingLabel');
    if (label) {
        label.innerText = rating > 0 ? `You rated: ${rating}/5 stars` : 'Click to rate';
    }
}

function rateMovie(movieId, rating) {
    const ratings = JSON.parse(localStorage.getItem('ratings') || '{}');
    ratings[movieId] = rating;
    localStorage.setItem('ratings', JSON.stringify(ratings));
    updateRatingDisplay(document.getElementById('ratingStars'), movieId);
    showNotification(`Rated ${rating}/5 stars!`, 'success');
}

function loadUserRating(movieId) {
    const container = document.getElementById('ratingStars');
    createRatingStars(container, movieId);
}

// ===== ENHANCED COMMENTS SYSTEM =====
function addComment() {
    const input = document.getElementById('commentInput');
    const text = input.value.trim();
    if (!text || !currentMovieId) return;

    const comments = JSON.parse(localStorage.getItem('comments') || '{}');
    if (!comments[currentMovieId]) comments[currentMovieId] = [];

    const comment = {
        text: text,
        timestamp: new Date().toISOString(),
        id: Date.now()
    };

    comments[currentMovieId].push(comment);
    localStorage.setItem('comments', JSON.stringify(comments));
    input.value = '';
    loadComments(currentMovieId);
    showNotification('Comment added!', 'success');
}

function loadComments(movieId) {
    const list = document.getElementById('commentList');
    list.innerHTML = '';
    const comments = JSON.parse(localStorage.getItem('comments') || '{}')[movieId] || [];

    // Update comment count
    const countElement = document.getElementById('commentCount');
    countElement.innerText = `${comments.length} comment${comments.length !== 1 ? 's' : ''}`;

    if (comments.length === 0) {
        list.innerHTML = '<li style="text-align: center; opacity: 0.5; padding: 2rem;">No comments yet. Be the first to share your thoughts!</li>';
        return;
    }

    comments.reverse().forEach(comment => {
        const li = document.createElement('li');
        li.className = 'comment-item';
        li.innerHTML = `
            <div class="comment-text">${comment.text}</div>
            <div class="comment-meta">
                <span>${formatDate(comment.timestamp)}</span>
                <button onclick="deleteComment(${movieId}, ${comment.id})" style="background: none; border: none; color: #ef4444; cursor: pointer; font-size: 0.75rem;">Delete</button>
            </div>
        `;
        list.appendChild(li);
    });
}

function deleteComment(movieId, commentId) {
    const comments = JSON.parse(localStorage.getItem('comments') || '{}');
    if (comments[movieId]) {
        comments[movieId] = comments[movieId].filter(c => c.id !== commentId);
        localStorage.setItem('comments', JSON.stringify(comments));
        loadComments(movieId);
        showNotification('Comment deleted', 'info');
    }
}

function formatDate(isoString) {
    const date = new Date(isoString);
    return date.toLocaleDateString() + ' at ' + date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
}

// ===== WATCHLIST FEATURE =====
function addToWatchlist() {
    if (!currentMovie) return;

    let watchlist = JSON.parse(localStorage.getItem('watchlist') || '[]');
    if (!watchlist.some(m => m.id === currentMovie.id)) {
        watchlist.push(currentMovie);
        localStorage.setItem('watchlist', JSON.stringify(watchlist));
        showNotification(`${currentMovie.title} added to watchlist!`, 'success');
    } else {
        showNotification('Movie already in watchlist', 'info');
    }
}

// ===== SHARE FEATURE =====
function shareMovie() {
    if (!currentMovie) return;

    if (navigator.share) {
        navigator.share({
            title: currentMovie.title,
            text: `Check out this movie: ${currentMovie.title}`,
            url: window.location.href
        });
    } else {
        // Fallback: copy to clipboard
        const shareText = `Check out this movie: ${currentMovie.title} - ${currentMovie.overview}`;
        navigator.clipboard.writeText(shareText).then(() => {
            showNotification('Movie details copied to clipboard!', 'success');
        });
    }
}

// ===== NOTIFICATION SYSTEM =====
function showNotification(message, type = 'info') {
    const notification = document.createElement('div');
    notification.className = `notification ${type}`;
    notification.innerHTML = `
        <div style="display: flex; align-items: center; gap: 0.5rem;">
            <span>${type === 'success' ? '✅' : type === 'error' ? '❌' : 'ℹ️'}</span>
            <span>${message}</span>
        </div>
    `;

    document.body.appendChild(notification);

    setTimeout(() => {
        notification.style.animation = 'slideInRight 0.3s ease reverse';
        setTimeout(() => {
            document.body.removeChild(notification);
        }, 300);
    }, 3000);
}

// ===== STATS UPDATE =====
function updateStats() {
    const favorites = JSON.parse(localStorage.getItem('favorites') || '[]');
    const ratings = JSON.parse(localStorage.getItem('ratings') || '{}');

    // Update favorites count
    document.getElementById('favoritesCount').innerText = favorites.length;

    // Calculate average rating
    const ratingValues = Object.values(ratings);
    const avgRating = ratingValues.length > 0
        ? (ratingValues.reduce((a, b) => a + b, 0) / ratingValues.length).toFixed(1)
        : '0.0';
    document.getElementById('avgRating').innerText = avgRating;
}

// ===== ENHANCED MOVIE CARD CREATION =====
function createMovieCard(movie, isFavorite = false) {
    const movieData = encodeURIComponent(JSON.stringify(movie))

    return `
        <div class="movie-poster-container">
            <img src="https://image.tmdb.org/t/p/w500${movie.poster_path}" alt="${movie.title}" loading="lazy">
            <div class="movie-overlay">
                <div class="quick-actions">
                    <button class="quick-action-btn" onclick='openModal(JSON.parse(decodeURIComponent("${movieData}")))'>
                        View Details
                    </button>
                    <button class="quick-action-btn" onclick='addToComparison(JSON.parse(decodeURIComponent("${movieData}")))'>
                        Compare
                    </button>
                    ${isFavorite ?
            `<button class="quick-action-btn" onclick="removeFavorite(${movie.id})" style="background: #ef4444; color: white;">Remove</button>` :
            `<button class="quick-action-btn" onclick='saveFavorite(JSON.parse(decodeURIComponent("${movieData}")))'>Add to Favorites</button>`
        }
                </div>
            </div>
        </div>
        <div class="movie-content">
            <h4>${movie.title}</h4>
            <div class="movie-rating">
                <span class="rating-badge">
                    <span>⭐</span>
                    ${movie.vote_average.toFixed(1)}
                </span>
            </div>
            <div class="movie-details">
                <span class="movie-year">${movie.release_date.split('-')[0]}</span>
                <span class="movie-language">${movie.original_language.toUpperCase()}</span>
            </div>
            <div class="movie-overview">${movie.overview || "No overview available."}</div>
        </div>
    `
}




// ===== INITIALIZE ON LOAD =====
document.addEventListener('DOMContentLoaded', function () {
    displayFavorites();
    if (typeof updateAdvancedStats === 'function') {
        updateAdvancedStats();
    } else {
        updateStats();
    }

    // Add keyboard shortcuts
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && document.getElementById('movieModal').style.display === 'block') {
            closeModal();
        }
    });
});

// ===== PERFORMANCE OPTIMIZATION =====
// Lazy loading for images
function setupLazyLoading() {
    const images = document.querySelectorAll('img[loading="lazy"]');
    const imageObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const img = entry.target;
                img.src = img.dataset.src || img.src;
                img.classList.remove('loading-skeleton');
                observer.unobserve(img);
            }
        });
    });

    images.forEach(img => imageObserver.observe(img));
}

// Call setup functions
setupLazyLoading();