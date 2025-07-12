// Language data for different languages
let currentFetchPage = 1;
const tmdbLanguageMap = {
    en: 'en-US',
    fr: 'fr-FR',
    es: 'es-ES',
    ar: 'ar-SA'
};
const languageData = {
    en: {
        title: "The Movie Vault",
        subtitle: "Discover amazing movies from around the world",
        selectCategory: "Select a category",
        getRecommendations: "Get Recommendations",
        enterYear: "Enter release year (e.g., 2020)",
        noCategory: "Please select a category.",
        loading: "Loading...",
        noRecommendations: "No new recommendations available for this year.",
        labels: {
            language: "Language",
            genre: "Genre",
            year: "Release Year",
            search: "Search Movies",
            totalMovies: "Movies Found",
            favorites: "Favorites",
            avgRating: "Avg Rating",
            favoritesTitle: "Your Favorite Movies",
            clearAll: "Clear All",
            rateMovie: "Rate this movie",
            comments: "Comments",
            addComment: "Add Comment",
            overview: "Overview"
        },
        categories: {
            action: "Action",
            comedy: "Comedy",
            drama: "Drama",
            horror: "Horror",
            'sci-fi': "Sci-Fi",
            romance: "Romance",
            thriller: "Thriller",
            fantasy: "Fantasy",
            animation: "Animation",
            documentary: "Documentary"
        },
        movieDetails: {
            rating: "Rating: ⭐",
            year: "Year",
            language: "Language"
        }
    },
    fr: {
        title: "Le Coffre à Films",
        subtitle: "Découvrez des films incroyables du monde entier",
        selectCategory: "Sélectionnez une catégorie",
        getRecommendations: "Obtenir des recommandations",
        enterYear: "Entrez l'année de sortie (par exemple, 2020)",
        noCategory: "Veuillez sélectionner une catégorie.",
        loading: "Chargement...",
        noRecommendations: "Aucune nouvelle recommandation disponible pour cette année.",
        labels: {
            language: "Langue",
            genre: "Genre",
            year: "Année de sortie",
            search: "Rechercher des films",
            totalMovies: "Films trouvés",
            favorites: "Favoris",
            avgRating: "Note moyenne",
            favoritesTitle: "Vos films favoris",
            clearAll: "Tout effacer",
            rateMovie: "Noter ce film",
            comments: "Commentaires",
            addComment: "Ajouter un commentaire",
            overview: "Synopsis"
        },
        categories: {
            action: "Action",
            comedy: "Comédie",
            drama: "Drame",
            horror: "Horreur",
            'sci-fi': "Sci-Fi",
            romance: "Romance",
            thriller: "Thriller",
            fantasy: "Fantasy",
            animation: "Animation",
            documentary: "Documentaire"
        },
        movieDetails: {
            rating: "Note : ⭐",
            year: "Année",
            language: "Langue"
        }
    },
    es: {
        title: "El Cofre de Películas",
        subtitle: "Descubre películas increíbles de todo el mundo",
        selectCategory: "Seleccionar una categoría",
        getRecommendations: "Obtener recomendaciones",
        enterYear: "Ingrese el año de lanzamiento (por ejemplo, 2020)",
        noCategory: "Por favor seleccione una categoría.",
        loading: "Cargando...",
        noRecommendations: "No hay nuevas recomendaciones disponibles para este año.",
        labels: {
            language: "Idioma",
            genre: "Género",
            year: "Año de lanzamiento",
            search: "Buscar películas",
            totalMovies: "Películas encontradas",
            favorites: "Favoritos",
            avgRating: "Calificación promedio",
            favoritesTitle: "Tus películas favoritas",
            clearAll: "Limpiar todo",
            rateMovie: "Calificar esta película",
            comments: "Comentarios",
            addComment: "Agregar comentario",
            overview: "Sinopsis"
        },
        categories: {
            action: "Acción",
            comedy: "Comedia",
            drama: "Drama",
            horror: "Horror",
            'sci-fi': "Ciencia ficción",
            romance: "Romántica",
            thriller: "Suspenso",
            fantasy: "Fantasía",
            animation: "Animación",
            documentary: "Documental"
        },
        movieDetails: {
            rating: "Calificación: ⭐",
            year: "Año",
            language: "Idioma"
        }
    },
    ar: {
        title: "صندوق الأفلام",
        subtitle: "اكتشف أفلاماً رائعة من جميع أنحاء العالم",
        selectCategory: "اختر الفئة",
        getRecommendations: "الحصول على التوصيات",
        enterYear: "أدخل سنة الإصدار (على سبيل المثال ، 2020)",
        noCategory: "الرجاء اختيار فئة.",
        loading: "جار التحميل...",
        noRecommendations: "لا توجد توصيات جديدة متاحة لهذا العام.",
        labels: {
            language: "اللغة",
            genre: "النوع",
            year: "سنة الإصدار",
            search: "البحث عن الأفلام",
            totalMovies: "الأفلام الموجودة",
            favorites: "المفضلة",
            avgRating: "متوسط التقييم",
            favoritesTitle: "أفلامك المفضلة",
            clearAll: "مسح الكل",
            rateMovie: "قيم هذا الفيلم",
            comments: "التعليقات",
            addComment: "إضافة تعليق",
            overview: "نظرة عامة"
        },
        categories: {
            action: "أكشن",
            comedy: "كوميديا",
            drama: "دراما",
            horror: "رعب",
            'sci-fi': "خيال علمي",
            romance: "رومانسية",
            thriller: "إثارة",
            fantasy: "فانتازيا",
            animation: "رسوم متحركة",
            documentary: "وثائقي"
        },
        movieDetails: {
            rating: "التقييم: ⭐",
            year: "السنة",
            language: "اللغة"
        }
    }
};
// ===== THEME TOGGLE FUNCTION =====
// ===== THEME TOGGLE FUNCTION =====
function toggleDarkMode() {
    const isDark = document.body.classList.contains('dark-mode');
    document.body.classList.toggle('dark-mode', !isDark);
    localStorage.setItem('theme', isDark ? 'light' : 'dark');
}

// Load saved theme on page load
(function applySavedTheme() {
    const savedTheme = localStorage.getItem('theme') || 'dark';
    if (savedTheme === 'dark') {
        document.body.classList.add('dark-mode');
    }
})();
// Default language is English
let currentLanguage = 'en';

// Function to update UI text based on selected language
function changeLanguage() {
    const selectedLang = document.getElementById("language").value;
    currentLanguage = selectedLang;

    const languageTexts = languageData[selectedLang];

    document.getElementById("title").innerText = languageTexts.title;
    document.getElementById("subtitle").innerText = languageTexts.subtitle;
    document.getElementById("category").options[0].innerText = languageTexts.selectCategory;
    document.getElementById("getRecommendationsBtn").innerText = languageTexts.getRecommendations;
    document.getElementById("year").placeholder = languageTexts.enterYear;
    document.getElementById("searchInput").placeholder = `🔍 ${languageTexts.labels.search}...`;
    document.getElementById("loadingText").innerText = languageTexts.loading;

    // Update labels
    const labels = languageTexts.labels;
    document.getElementById("languageLabel").innerText = labels.language;
    document.getElementById("categoryLabel").innerText = labels.genre;
    document.getElementById("yearLabel").innerText = labels.year;
    document.getElementById("searchLabel").innerText = labels.search;
    document.getElementById("totalMoviesLabel").innerText = labels.totalMovies;
    document.getElementById("favoritesCountLabel").innerText = labels.favorites;
    document.getElementById("avgRatingLabel").innerText = labels.avgRating;
    document.getElementById("favoritesTitle").innerHTML = `<span>⭐</span> ${labels.favoritesTitle}`;
    document.getElementById("clearFavoritesBtn").innerText = labels.clearAll;
    document.getElementById("rateMovieLabel").innerText = labels.rateMovie;
    document.getElementById("commentsLabel").innerText = labels.comments;
    document.getElementById("addCommentBtn").innerText = labels.addComment;
    document.getElementById("modalOverviewLabel").innerText = labels.overview;

    // Set text direction for Arabic (rtl) or other languages (ltr)
    document.body.style.direction = selectedLang === 'ar' ? 'rtl' : 'ltr';

    // Update category names dynamically based on selected language
    const categorySelect = document.getElementById("category");
    const categories = languageTexts.categories;
    const options = categorySelect.querySelectorAll("option");

    // Skip the first option (placeholder), update the rest
    for (let i = 1; i < options.length; i++) {
        const categoryValue = options[i].value;
        options[i].innerText = categories[categoryValue] || categoryValue; // Default to category value if translation is missing
    }

    // Update comment input placeholder
    document.getElementById("commentInput").placeholder = selectedLang === 'en' ?
        "Share your thoughts about this movie..." :
        selectedLang === 'fr' ? "Partagez vos impressions sur ce film..." :
            selectedLang === 'es' ? "Comparte tus pensamientos sobre esta película..." :
                "شاركنا رأيك في هذا الفيلم...";
}

// Function to fetch movie recommendations with a filter for explicit content
const shownMovies = new Set();

async function fetchRecommendations(genreId, year, page = 1, language = 'en') {
    const apiKey = '8825c8c0021512537b2c6b70cad7d7e1';
    const languageCode = tmdbLanguageMap[language] || 'en-US';


    const maxPage = 500;
    const randomPage = Math.floor(Math.random() * maxPage) + 1;

    let url = `https://api.themoviedb.org/3/discover/movie?api_key=${apiKey}&with_genres=${genreId}&sort_by=popularity.desc&page=${page}&certification_country=US&language=${languageCode}`;

    if (year) {
        url += `&primary_release_year=${year}`;
    }

    // Remove this — language is already added above
    // if (language) {
    //     url += `&language=${language}`;
    // }

    url += '&certification.lte=PG-13';

    const response = await fetch(url);
    const data = await response.json();

    console.log('Fetched movies:', data.results);
    return data.results;
}


/**
 * Updates basic stats in the UI based on current favorites.
 * - Total number of favorite movies
 * - Average rating of all favorites
 */
function updateStats() {
    const favorites = JSON.parse(localStorage.getItem('favorites') || '[]');

    // Update total favorites count
    const favoritesCountEl = document.getElementById('favoritesCount');
    if (favoritesCountEl) {
        favoritesCountEl.innerText = favorites.length;
    }

    // Calculate average rating
    const avgRatingEl = document.getElementById('avgRating');
    if (avgRatingEl && favorites.length > 0) {
        const totalRating = favorites.reduce((sum, fav) => sum + (fav.rating || 0), 0);
        const avgRating = (totalRating / favorites.length).toFixed(1);
        avgRatingEl.innerText = avgRating;
    } else if (avgRatingEl) {
        avgRatingEl.innerText = '0.0';
    }
}

/**
 * Updates advanced stats in the UI based on current favorites.
 * - Total comments across all favorites
 * - Total watch time approximation (if available)
 * - Genre distribution chart (stubbed for future extension)
 */
function updateAdvancedStats() {
    const favorites = JSON.parse(localStorage.getItem('favorites') || '[]');

    // Basic stats
    const favoritesCountEl = document.getElementById('favoritesCount');
    if (favoritesCountEl) {
        favoritesCountEl.innerText = favorites.length;
    }

    let totalRating = 0;
    let totalComments = 0;

    favorites.forEach(fav => {
        totalRating += fav.rating || 0;
        totalComments += fav.comments ? fav.comments.length : 0;
    });

    const avgRatingEl = document.getElementById('avgRating');
    if (avgRatingEl) {
        const avgRating = favorites.length > 0 ? (totalRating / favorites.length).toFixed(1) : '0.0';
        avgRatingEl.innerText = avgRating;
    }

    const totalCommentsEl = document.getElementById('totalComments');
    if (totalCommentsEl) {
        totalCommentsEl.innerText = totalComments;
    }

    // Optional: Stub for genre distribution or other analytics
    const genreDistributionEl = document.getElementById('genreDistribution');
    if (genreDistributionEl) {
        const genreMap = {};
        favorites.forEach(fav => {
            if (fav.genre && fav.genre.length) {
                fav.genre.forEach(genre => {
                    genreMap[genre] = (genreMap[genre] || 0) + 1;
                });
            }
        });
        genreDistributionEl.innerText = Object.entries(genreMap)
            .sort((a, b) => b[1] - a[1])
            .map(([genre, count]) => `${genre}: ${count}`)
            .join('\n');
    }
}
function displayMovies(movies) {
    const container = document.getElementById('recommendations');
    container.innerHTML = ''; // clear old results

    movies.forEach(movie => {
        const li = document.createElement('li');
        li.textContent = movie.title; // or better movie card markup
        container.appendChild(li);
    });
}

function createMovieCard(movie) {
    return `
        <div class="movie-card">
<img src="https://image.tmdb.org/t/p/w300${movie.poster_path}" alt="${movie.title}">
            <h3>${movie.title}</h3>
            <p>⭐ ${movie.vote_average}</p>
        </div>
    `;
}
// Function to show recommendations
async function showRecommendations() {
    shownMovies.clear();
    const categoryMap = {
        action: 28,
        comedy: 35,
        drama: 18,
        horror: 27,
        'sci-fi': 878,
        romance: 10749,
        thriller: 53,
        fantasy: 14,
        animation: 16,
        documentary: 99
    };

    const category = document.getElementById("category").value;
    const year = document.getElementById("year").value;
    const language = currentLanguage;  // Use the current language selected by the user
    const recommendationsList = document.getElementById("recommendations");
    const loader = document.getElementById("loader");

    recommendationsList.innerHTML = '';
    loader.style.display = 'block'; // Show loader

    if (category) {
        const genreId = categoryMap[category];
        let movies = [];
        let page = currentFetchPage; // use global page number

        // Fetch movies until we have at least 24 unique ones
        while (movies.length < 24) {
            const fetchedMovies = await fetchRecommendations(genreId, year, page, language);
            if (fetchedMovies.length === 0) break;

            const newMovies = fetchedMovies.filter(movie => !shownMovies.has(movie.id));

            newMovies.forEach(movie => shownMovies.add(movie.id)); // add immediately

            movies = [...movies, ...newMovies];
            page++;
        }
        currentFetchPage = page;  // save for next call


        // Sort movies by rating in descending order
        const sortedMovies = movies.sort((a, b) => b.vote_average - a.vote_average);

        // Select the first 21 movies
        const selectedMovies = sortedMovies.slice(0, 20);

        selectedMovies.forEach((movie, index) => {
            const listItem = document.createElement("li");
            listItem.className = 'recommendation-item';
            listItem.style.setProperty('--index', index);
            listItem.innerHTML = createMovieCard(movie);
            recommendationsList.appendChild(listItem);
            shownMovies.add(movie.id); // Mark the movie as shown
        });

        // Update stats
        document.getElementById('totalMovies').innerText = selectedMovies.length;
        updateStats();
    } else {
        alert(languageData[currentLanguage].noCategory);
    }

    loader.style.display = 'none'; // Hide loader
}
