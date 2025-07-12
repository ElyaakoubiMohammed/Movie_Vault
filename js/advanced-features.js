
let trendingMovies = [];
let currentTrendingIndex = 0;
let selectedMood = null;
let quizQuestions = [];
let currentQuizIndex = 0;
let quizScore = 0;
let comparisonMovies = { movie1: null, movie2: null };
let movieCollections = [];
const totalQuestions = 40; // Total number of quiz questions
// ===== THEME TOGGLE =====
let isDarkMode = localStorage.getItem('theme') !== 'light';

function toggleTheme() {
    isDarkMode = !isDarkMode;
    document.body.classList.toggle('light-mode', !isDarkMode);
    localStorage.setItem('theme', isDarkMode ? 'dark' : 'light');
    document.getElementById('themeIcon').textContent = isDarkMode ? '🌙' : '☀️';
    showNotification(`${isDarkMode ? 'Dark' : 'Light'} mode enabled`, 'info');
}

// Initialize theme
(function initTheme() {
    document.body.classList.toggle('light-mode', !isDarkMode);
    document.getElementById('themeIcon').textContent = isDarkMode ? '🌙' : '☀️';
})();

// ===== NAVIGATION =====
function scrollToSection(sectionId) {
    const section = document.getElementById(sectionId);
    if (section) {
        section.scrollIntoView({ behavior: 'smooth' });

        // Update active nav link
        document.querySelectorAll('.nav-link').forEach(link => {
            link.classList.remove('active');
            if (link.getAttribute('href') === `#${sectionId}`) {
                link.classList.add('active');
            }
        });
    }
}

// Navbar scroll effect
window.addEventListener('scroll', () => {
    const navbar = document.getElementById('navbar');
    if (window.scrollY > 100) {
        navbar.classList.add('scrolled');
    } else {
        navbar.classList.remove('scrolled');
    }
});

// ===== TRENDING CAROUSEL =====
async function loadTrendingMovies() {
    try {
        const apiKey = '8825c8c0021512537b2c6b70cad7d7e1';
        const response = await fetch(`https://api.themoviedb.org/3/trending/movie/week?api_key=${apiKey}`);
        const data = await response.json();
        trendingMovies = data.results.slice(0, 10);
        displayTrendingMovies();
    } catch (error) {
        console.error('Error loading trending movies:', error);
    }
}

function displayTrendingMovies() {
    const track = document.getElementById('trendingTrack');
    track.innerHTML = '';

    trendingMovies.forEach(movie => {
        const item = document.createElement('div');
        item.className = 'trending-item';
        item.onclick = () => openModal(movie);

        const backdropUrl = movie.backdrop_path
            ? `https://image.tmdb.org/t/p/w1280${movie.backdrop_path}`
            : `https://image.tmdb.org/t/p/w500${movie.poster_path}`;

        item.innerHTML = `
              <img class="trending-backdrop" src="${backdropUrl}" alt="${movie.title}">
              <div class="trending-overlay">
                  <div class="trending-title">${movie.title}</div>
                  <div class="trending-meta">
                      <span>⭐ ${movie.vote_average.toFixed(1)}</span>
                      <span>${movie.release_date.split('-')[0]}</span>
                      <span>${movie.original_language.toUpperCase()}</span>
                  </div>
              </div>
          `;
        track.appendChild(item);
    });
}

function moveTrendingCarousel(direction) {
    const track = document.getElementById('trendingTrack');
    const itemWidth = 300 + 16; // width + gap
    currentTrendingIndex += direction;

    if (currentTrendingIndex < 0) currentTrendingIndex = 0;
    if (currentTrendingIndex > trendingMovies.length - 3) currentTrendingIndex = trendingMovies.length - 3;

    track.style.transform = `translateX(-${currentTrendingIndex * itemWidth}px)`;
}

// ===== GENRE PILLS =====
function initializeGenrePills() {
    const pills = document.querySelectorAll('.genre-pill');
    pills.forEach(pill => {
        pill.addEventListener('click', () => {
            pills.forEach(p => p.classList.remove('active'));
            pill.classList.add('active');

            const genre = pill.dataset.genre;
            document.getElementById('category').value = genre;

            if (genre) {
                showRecommendations();
            }
        });
    });
}

// ===== ADVANCED SEARCH FILTERS =====
function initializeAdvancedFilters() {
    const ratingRange = document.getElementById('ratingRange');
    const runtimeRange = document.getElementById('runtimeRange');

    ratingRange.addEventListener('input', (e) => {
        document.getElementById('ratingValue').textContent = e.target.value;
    });

    runtimeRange.addEventListener('input', (e) => {
        const value = e.target.value;
        document.getElementById('runtimeValue').textContent = value === '300' ? '300+' : value;
    });
}

// ===== AI RECOMMENDATION ENGINE =====
function initializeMoodSelector() {
    const cards = document.querySelectorAll('.preference-card');
    cards.forEach(card => {
        card.addEventListener('click', () => {
            cards.forEach(c => c.classList.remove('selected'));
            card.classList.add('selected');
            selectedMood = card.dataset.mood;
            generateMoodBasedRecommendations();
        });
    });
}

async function generateMoodBasedRecommendations() {
    if (!selectedMood) return;

    const moodGenreMap = {
        action: 'action',
        emotional: 'drama',
        funny: 'comedy',
        mindblowing: 'sci-fi',
        romantic: 'romance',
        scary: 'horror'
    };

    const genre = moodGenreMap[selectedMood];
    document.getElementById('category').value = genre;

    showNotification(`Finding ${selectedMood} movies for you...`, 'info');
    await showRecommendations();
    scrollToSection('discover');
}

// ===== INTERACTIVE TABS =====
function switchTab(tabName) {
    // Update tab buttons
    document.querySelectorAll('.tab-button').forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');

    // Update tab content
    document.querySelectorAll('.tab-content').forEach(content => content.classList.remove('active'));
    document.getElementById(tabName).classList.add('active');

    // Load content based on tab
    switch (tabName) {
        case 'quiz':
            loadQuizQuestions();
            break;
        case 'timeline':
            generateMovieTimeline();
            break;
        case 'social':
            loadSocialFeed();
            break;
    }
}

// ===== MOVIE QUIZ =====
function loadQuizQuestions() {
    quizQuestions = [
        {
            question: "Which movie won the Academy Award for Best Picture in 2020?",
            options: ["1917", "Joker", "Parasite", "Once Upon a Time in Hollywood"],
            correct: 2
        },
        {
            question: "Who directed the movie 'Inception'?",
            options: ["Steven Spielberg", "Christopher Nolan", "Martin Scorsese", "Quentin Tarantino"],
            correct: 1
        },
        {
            question: "Which movie features the quote 'May the Force be with you'?",
            options: ["Star Trek", "Star Wars", "Guardians of the Galaxy", "Interstellar"],
            correct: 1
        },
        {
            question: "What is the highest-grossing movie of all time?",
            options: ["Titanic", "Avatar", "Avengers: Endgame", "Star Wars: The Force Awakens"],
            correct: 2
        },
        {
            question: "Which animated movie features the song 'Let It Go'?",
            options: ["Moana", "Tangled", "Frozen", "Encanto"],
            correct: 2
        }
    ];

    currentQuizIndex = 0;
    quizScore = 0;
    displayQuizQuestion();
}

function displayQuizQuestion() {
    for (let i = 1; i <= totalQuestions; i++) {
        const quizDiv = document.getElementById(`quiz${i}`);
        if (!quizDiv) continue; // Skip if the div doesn't exist
        if (i === currentQuizIndex) {
            quizDiv.classList.add('active');
            quizDiv.style.display = 'block'; // Show current question
        } else {
            quizDiv.classList.remove('active');
            quizDiv.style.display = 'none'; // Hide others
        }
    }
}

function selectQuizOption(element, isCorrect) {
    // Disable all options
    document.querySelectorAll('.quiz-option').forEach(option => {
        option.onclick = null;
        option.style.pointerEvents = 'none';
    });

    // Show correct/incorrect
    element.classList.add(isCorrect ? 'correct' : 'incorrect');

    if (isCorrect) {
        quizScore++;
        showNotification('Correct! 🎉', 'success');
    } else {
        showNotification('Incorrect 😔', 'error');
        // Highlight correct answer
        const correctIndex = quizQuestions[currentQuizIndex].correct;
        document.querySelectorAll('.quiz-option')[correctIndex].classList.add('correct');
    }

    document.getElementById('nextQuizBtn').style.display = 'block';
}

function nextQuizQuestion() {
    currentQuizIndex++;
    if (currentQuizIndex > totalQuestions) {
        currentQuizIndex = 1; // Loop back to first question after last
    }
    displayQuizQuestion();
}

function showQuizResults() {
    const percentage = Math.round((quizScore / quizQuestions.length) * 100);
    document.getElementById('quizQuestion').textContent = `Quiz Complete! You scored ${quizScore}/${quizQuestions.length} (${percentage}%)`;
    document.getElementById('quizOptions').innerHTML = `
          <div style="text-align: center; padding: 2rem;">
              <div style="font-size: 4rem; margin-bottom: 1rem;">
                  ${percentage >= 80 ? '🏆' : percentage >= 60 ? '🎉' : '📚'}
              </div>
              <p style="font-size: 1.2rem; margin-bottom: 1rem;">
                  ${percentage >= 80 ? 'Movie Expert!' : percentage >= 60 ? 'Well Done!' : 'Keep Learning!'}
              </p>
              <button class="btn-primary" onclick="loadQuizQuestions()" style="max-width: 200px;">Try Again</button>
          </div>
      `;
    document.getElementById('nextQuizBtn').style.display = 'none';
}

// ===== MOVIE COMPARISON =====
function selectMovieForComparison(slot) {
    showNotification('Click on any movie in the recommendations to add it to comparison', 'info');
    // This will be handled by the movie card click events
}

function addToComparison(movie) {
    if (!comparisonMovies.movie1) {
        comparisonMovies.movie1 = movie;
        updateComparisonDisplay(1, movie);
    } else if (!comparisonMovies.movie2) {
        comparisonMovies.movie2 = movie;
        updateComparisonDisplay(2, movie);
        showComparison();
    } else {
        // Replace movie1 and shift movie2 to movie1
        comparisonMovies.movie1 = comparisonMovies.movie2;
        comparisonMovies.movie2 = movie;
        updateComparisonDisplay(1, comparisonMovies.movie1);
        updateComparisonDisplay(2, movie);
        showComparison();
    }
}

function updateComparisonDisplay(slot, movie) {
    document.getElementById(`compareMovie${slot}Poster`).src = `https://image.tmdb.org/t/p/w500${movie.poster_path}`;
    document.getElementById(`compareMovie${slot}Title`).textContent = movie.title;
    document.getElementById(`compareMovie${slot}Stats`).innerHTML = `
          <div>⭐ ${movie.vote_average.toFixed(1)}/10</div>
          <div>📅 ${movie.release_date.split('-')[0]}</div>
          <div>🌍 ${movie.original_language.toUpperCase()}</div>
      `;
}

function showComparison() {
    if (comparisonMovies.movie1 && comparisonMovies.movie2) {
        const movie1 = comparisonMovies.movie1;
        const movie2 = comparisonMovies.movie2;

        let winner = '';
        if (movie1.vote_average > movie2.vote_average) {
            winner = `${movie1.title} has a higher rating!`;
        } else if (movie2.vote_average > movie1.vote_average) {
            winner = `${movie2.title} has a higher rating!`;
        } else {
            winner = "It's a tie in ratings!";
        }

        showNotification(winner, 'info');
    }
}

// ===== MOVIE TIMELINE =====
async function generateMovieTimeline() {
    const timeline = document.getElementById('movieTimeline');
    timeline.innerHTML = '<div class="loader"><div class="loader-spinner"></div></div>';

    try {
        const apiKey = '8825c8c0021512537b2c6b70cad7d7e1';
        const years = [2020, 2021, 2022, 2023, 2024];
        const timelineData = [];

        for (const year of years) {
            const response = await fetch(`https://api.themoviedb.org/3/discover/movie?api_key=${apiKey}&primary_release_year=${year}&sort_by=vote_average.desc&vote_count.gte=1000`);
            const data = await response.json();
            if (data.results.length > 0) {
                timelineData.push({
                    year,
                    movie: data.results[0]
                });
            }
        }

        timeline.innerHTML = '';
        timelineData.forEach((item, index) => {
            const timelineItem = document.createElement('div');
            timelineItem.className = 'timeline-item';
            timelineItem.innerHTML = `
                  <div class="timeline-year">${item.year}</div>
                  <div class="timeline-content">
                      <h4>${item.movie.title}</h4>
                      <p>⭐ ${item.movie.vote_average.toFixed(1)} | ${item.movie.release_date}</p>
                      <p>${item.movie.overview.substring(0, 150)}...</p>
                      <button class="btn-primary" onclick="openModal(${JSON.stringify(item.movie).replace(/"/g, '"')})" style="margin-top: 1rem; max-width: 150px;">View Details</button>
                  </div>
              `;
            timeline.appendChild(timelineItem);
        });
    } catch (error) {
        timeline.innerHTML = '<p style="text-align: center; color: var(--text-secondary);">Failed to load timeline</p>';
    }
}

// ===== SOCIAL FEED =====
function loadSocialFeed() {
    const feed = document.getElementById('socialFeed');
    const posts = [
        {
            user: 'MovieBuff2024',
            avatar: 'M',
            time: '2 hours ago',
            content: 'Just watched "Dune: Part Two" and I\'m absolutely blown away! The cinematography is stunning and Hans Zimmer\'s score is phenomenal. 🎬✨',
            likes: 24,
            comments: 8
        },
        {
            user: 'CinemaLover',
            avatar: 'C',
            time: '5 hours ago',
            content: 'Anyone else think "Everything Everywhere All at Once" deserved every Oscar it won? Such a creative and emotional masterpiece! 🏆',
            likes: 42,
            comments: 15
        },
        {
            user: 'FilmCritic',
            avatar: 'F',
            time: '1 day ago',
            content: 'Hot take: "The Batman" (2022) is the best Batman movie since "The Dark Knight". Robert Pattinson absolutely nailed it! 🦇',
            likes: 67,
            comments: 23
        },
        {
            user: 'SciFiFan',
            avatar: 'S',
            time: '2 days ago',
            content: 'Rewatching "Blade Runner 2049" for the 5th time. This movie gets better with every viewing. The visual storytelling is unmatched! 🤖',
            likes: 31,
            comments: 12
        }
    ];

    feed.innerHTML = '';
    posts.forEach(post => {
        const postElement = document.createElement('div');
        postElement.className = 'social-post';
        postElement.innerHTML = `
              <div class="post-header">
                  <div class="post-avatar">${post.avatar}</div>
                  <div class="post-user">${post.user}</div>
                  <div class="post-time">${post.time}</div>
              </div>
              <div class="post-content">${post.content}</div>
              <div class="post-actions">
                  <button class="post-action" onclick="likePost(this)">
                      <span>👍</span> ${post.likes}
                  </button>
                  <button class="post-action">
                      <span>💬</span> ${post.comments}
                  </button>
                  <button class="post-action">
                      <span>📤</span> Share
                  </button>
              </div>
          `;
        feed.appendChild(postElement);
    });
}

function likePost(button) {
    const currentLikes = parseInt(button.textContent.match(/\d+/)[0]);
    button.innerHTML = `<span>👍</span> ${currentLikes + 1}`;
    button.style.color = 'var(--accent-color)';
    showNotification('Post liked! 👍', 'success');
}

// ===== MOVIE COLLECTIONS =====
async function loadMovieCollections() {
    const collections = [
        {
            title: "Marvel Cinematic Universe",
            description: "The complete MCU saga",
            backdrop: "https://image.tmdb.org/t/p/w1280/9BBTo63ANSmhC4e6r62OJFuK2GL.jpg",
            count: 30,
            movies: []
        },
        {
            title: "Studio Ghibli Classics",
            description: "Magical animated masterpieces",
            backdrop: "https://image.tmdb.org/t/p/w1280/6KErczPBROQty7QoIsaa6wJYXZi.jpg",
            count: 12,
            movies: []
        },
        {
            title: "Christopher Nolan Films",
            description: "Mind-bending cinema",
            backdrop: "https://image.tmdb.org/t/p/w1280/qmDpIHrmpJINaRKAfWQfftjCdyi.jpg",
            count: 11,
            movies: []
        },
        {
            title: "Best Picture Winners",
            description: "Academy Award winners",
            backdrop: "https://image.tmdb.org/t/p/w1280/rAiYTfKGqDCRIIqo664sY9XZIvQ.jpg",
            count: 25,
            movies: []
        }
    ];

    const grid = document.getElementById('collectionGrid');
    grid.innerHTML = '';

    collections.forEach(collection => {
        const card = document.createElement('div');
        card.className = 'collection-card';
        card.onclick = () => showCollection(collection);
        card.innerHTML = `
              <div class="collection-header">
                  <img class="collection-backdrop" src="${collection.backdrop}" alt="${collection.title}">
                  <div class="collection-overlay">
                      <div class="collection-title">${collection.title}</div>
                      <div class="collection-count">${collection.count} movies</div>
                  </div>
              </div>
              <div class="collection-movies">
                  <p style="color: var(--text-secondary); margin-bottom: 1rem;">${collection.description}</p>
                  <div class="collection-preview">
                      ${Array(4).fill().map(() => `<div class="preview-poster skeleton-loader"></div>`).join('')}
                  </div>
              </div>
          `;
        grid.appendChild(card);
    });
}

function showCollection(collection) {
    showNotification(`Loading ${collection.title} collection...`, 'info');
    // This would typically load and display the collection movies
    // For now, we'll just show a notification
    setTimeout(() => {
        showNotification(`${collection.title} collection loaded!`, 'success');
    }, 1000);
}

// ===== ENHANCED STATS =====
function updateAdvancedStats() {
    const favorites = JSON.parse(localStorage.getItem('favorites') || '[]');
    const ratings = JSON.parse(localStorage.getItem('ratings') || '{}');
    const watchlist = JSON.parse(localStorage.getItem('watchlist') || '[]');

    // Calculate total watch time (estimate 2 hours per movie)
    const totalWatchTime = (favorites.length + Object.keys(ratings).length) * 2;
    document.getElementById('watchTime').textContent = `${totalWatchTime}h`;

    // Update other stats
    updateStats();
}

// ===== ENHANCED MOVIE CARDS =====
function createEnhancedMovieCard(movie, isFavorite = false) {
    return `
          <div class="movie-poster-container">
              <img src="https://image.tmdb.org/t/p/w500${movie.poster_path}" alt="${movie.title}" loading="lazy">
              <div class="movie-overlay">
                  <div class="quick-actions">
                      <button class="quick-action-btn" onclick="openModal(${JSON.stringify(movie).replace(/"/g, '"')})">
                          Details
                      </button>
                      <button class="quick-action-btn" onclick="addToComparison(${JSON.stringify(movie).replace(/"/g, '"')})">
                          Compare
                      </button>
                      ${isFavorite ?
            `<button class="quick-action-btn" onclick="removeFavorite(${movie.id})" style="background: #ef4444; color: white;">Remove</button>` :
            `<button class="quick-action-btn" onclick="saveFavorite(${JSON.stringify(movie).replace(/"/g, '"')})">Favorite</button>`
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
      `;
}

// ===== INITIALIZATION =====
document.addEventListener('DOMContentLoaded', function () {
    // Initialize all advanced features
    loadTrendingMovies();
    initializeGenrePills();
    initializeAdvancedFilters();
    initializeMoodSelector();
    loadMovieCollections();
    updateAdvancedStats();

    // Load default tab content
    loadQuizQuestions();

    // Add smooth scrolling to nav links
    document.querySelectorAll('.nav-link').forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            const targetId = link.getAttribute('href').substring(1);
            scrollToSection(targetId);
        });
    });

    // Initialize intersection observer for nav highlighting
    const sections = document.querySelectorAll('section[id]');
    const navLinks = document.querySelectorAll('.nav-link');

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                navLinks.forEach(link => {
                    link.classList.remove('active');
                    if (link.getAttribute('href') === `#${entry.target.id}`) {
                        link.classList.add('active');
                    }
                });
            }
        });
    }, { threshold: 0.3 });

    sections.forEach(section => observer.observe(section));
});

// ===== PERFORMANCE OPTIMIZATIONS =====
// Debounce function for search
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Lazy loading for images
const imageObserver = new IntersectionObserver((entries, observer) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            const img = entry.target;
            if (img.dataset.src) {
                img.src = img.dataset.src;
                img.removeAttribute('data-src');
                observer.unobserve(img);
            }
        }
    });
});

// Apply lazy loading to images
function applyLazyLoading() {
    const images = document.querySelectorAll('img[data-src]');
    images.forEach(img => imageObserver.observe(img));
}

// ===== KEYBOARD SHORTCUTS =====
document.addEventListener('keydown', (e) => {
    // ESC to close modal
    if (e.key === 'Escape' && document.getElementById('movieModal').style.display === 'block') {
        closeModal();
    }

    // Ctrl/Cmd + K for search
    if ((e.ctrlKey || e.metaKey) && e.key === 'k') {
        e.preventDefault();
        document.getElementById('searchInput').focus();
    }

    // T for theme toggle
    if (e.key === 't' && !e.ctrlKey && !e.metaKey && document.activeElement.tagName !== 'INPUT' && document.activeElement.tagName !== 'TEXTAREA') {
        toggleTheme();
    }
});

// ===== EXPORT FUNCTIONS FOR GLOBAL ACCESS =====
window.advancedFeatures = {
    toggleTheme,
    scrollToSection,
    moveTrendingCarousel,
    switchTab,
    selectQuizOption,
    nextQuizQuestion,
    addToComparison,
    likePost,
    showCollection
};