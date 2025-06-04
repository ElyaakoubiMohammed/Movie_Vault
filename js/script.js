// Language data for different languages
const languageData = {
    en: {
        title: "The Movie Vault",
        selectCategory: "Select a category",
        getRecommendations: "Get Recommendations",
        enterYear: "Enter release year (e.g., 2020)",
        noCategory: "Please select a category.",
        loading: "Loading...",
        noRecommendations: "No new recommendations available for this year.",
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
        selectCategory: "Sélectionnez une catégorie",
        getRecommendations: "Obtenir des recommandations",
        enterYear: "Entrez l'année de sortie (par exemple, 2020)",
        noCategory: "Veuillez sélectionner une catégorie.",
        loading: "Chargement...",
        noRecommendations: "Aucune nouvelle recommandation disponible pour cette année.",
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
        selectCategory: "Seleccionar una categoría",
        getRecommendations: "Obtener recomendaciones",
        enterYear: "Ingrese el año de lanzamiento (por ejemplo, 2020)",
        noCategory: "Por favor seleccione una categoría.",
        loading: "Cargando...",
        noRecommendations: "No hay nuevas recomendaciones disponibles para este año.",
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
        selectCategory: "اختر الفئة",
        getRecommendations: "الحصول على التوصيات",
        enterYear: "أدخل سنة الإصدار (على سبيل المثال ، 2020)",
        noCategory: "الرجاء اختيار فئة.",
        loading: "جار التحميل...",
        noRecommendations: "لا توجد توصيات جديدة متاحة لهذا العام.",
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
    document.getElementById("category").options[0].innerText = languageTexts.selectCategory;
    document.getElementById("getRecommendationsBtn").innerText = languageTexts.getRecommendations;
    document.getElementById("year").placeholder = languageTexts.enterYear;

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

    // Update movie details labels (rating, year, language)
    const movieDetails = languageTexts.movieDetails;
    document.getElementById("ratingLabel").innerText = movieDetails.rating;
    document.getElementById("yearLabel").innerText = movieDetails.year;
    document.getElementById("languageLabel").innerText = movieDetails.language;
}

// Function to fetch movie recommendations with a filter for explicit content
const shownMovies = new Set();

async function fetchRecommendations(genreId, year, page = 1, language = 'en') {
    const apiKey = '8825c8c0021512537b2c6b70cad7d7e1';
    let url = `https://api.themoviedb.org/3/discover/movie?api_key=${apiKey}&with_genres=${genreId}&sort_by=popularity.desc&page=${page}&certification_country=US`;

    if (year) {
        url += `&primary_release_year=${year}`;
    }

    if (language) {
        url += `&language=${language}`; // Adding the language filter to the API request
    }

    // Filter out explicit content (e.g., rated 'NC-17')
    url += '&certification.lte=PG-13'; // Filter out explicit content with certifications higher than PG-13

    const response = await fetch(url);
    const data = await response.json();
    return data.results;
}

// Function to show recommendations
async function showRecommendations() {
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
        let page = 1;

        // Fetch movies until we have at least 24 unique ones
        while (movies.length < 24) {
            const fetchedMovies = await fetchRecommendations(genreId, year, page, language);
            const newMovies = fetchedMovies.filter(movie => !shownMovies.has(movie.id));
            movies = [...movies, ...newMovies]; // Add new movies to the list
            page++; // Go to the next page

            if (fetchedMovies.length === 0) break; // Stop if no more movies are returned
        }

        // Sort movies by rating in descending order
        const sortedMovies = movies.sort((a, b) => b.vote_average - a.vote_average);

        // Select the first 21 movies
        const selectedMovies = sortedMovies.slice(0, 24);

        selectedMovies.forEach(movie => {
            const listItem = document.createElement("li");
            listItem.className = 'recommendation-item';
            listItem.innerHTML = `
                <img src="https://image.tmdb.org/t/p/w500${movie.poster_path}" alt="${movie.title}">
                <h4>${movie.title}</h4>
                <div class="movie-rating" id="ratingLabel">${languageData[currentLanguage].movieDetails.rating} ${movie.vote_average.toFixed(1)} / 10</div>
                <div class="movie-details">
                    <span id="yearLabel">${languageData[currentLanguage].movieDetails.year}: ${movie.release_date.split('-')[0]}</span> |
                    <span id="languageLabel">${languageData[currentLanguage].movieDetails.language}: ${movie.original_language.toUpperCase()}</span>
                </div>
                <div class="movie-overview">${movie.overview ? movie.overview : "No overview available."}</div>
            `;
            listItem.onclick = () => openModal(movie);
            recommendationsList.appendChild(listItem);
            shownMovies.add(movie.id); // Mark the movie as shown
        });
    } else {
        alert(languageData[currentLanguage].noCategory);
    }

    loader.style.display = 'none'; // Hide loader
}
