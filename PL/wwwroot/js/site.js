function filterProducts() {
    let searchQuery = document.getElementById("searchBar").value.toLowerCase();
    let selectedCategory = document.getElementById("categoryFilter").value;
    let productCards = document.querySelectorAll(".product-card");

    productCards.forEach(card => {
        let productName = card.getAttribute("data-name").toLowerCase();
        let productCategory = card.getAttribute("data-category");

        let matchesSearch = productName.includes(searchQuery);
        let matchesCategory = selectedCategory === "all" || selectedCategory === productCategory;

        if (matchesSearch && matchesCategory) {
            card.style.display = "block";
        } else {
            card.style.display = "none";
        }
    });

    // Move selected category to top
    let categorySections = document.querySelectorAll(".category-section");
    categorySections.forEach(section => {
        if (selectedCategory === "all" || section.getAttribute("data-category") === selectedCategory) {
            section.style.display = "block";
        } else {
            section.style.display = "none";
        }
    });
}
