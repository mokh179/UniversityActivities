    const form = document.getElementById("filterForm");
    const tableContainer = document.getElementById("activitiesContainer");
    const activeFilters = document.getElementById("activeFilters");

    form.addEventListener("change", applyFilter);
    form.addEventListener("keyup", debounce(applyFilter, 500));

    function applyFilter() {
    const formData = new FormData(form);
    const params = new URLSearchParams(formData);

    renderActiveFilters(formData);

    fetch(`?handler=Filter&${params}`)
        .then(r => r.text())
        .then(html => tableContainer.innerHTML = html);
}

    function renderActiveFilters(formData) {
        activeFilters.innerHTML = "";

    for (const [key, value] of formData.entries()) {
        if (!value) continue;

    const chip = document.createElement("span");
    chip.className = "badge bg-primary me-2";
    chip.innerHTML = `${key}: ${value} ✕`;

        chip.onclick = () => {
        form.querySelector(`[name="${key}"]`).value = "";
    applyFilter();
        };

    activeFilters.appendChild(chip);
    }
}

    function debounce(fn, delay) {
        let timer;
    return () => {
        clearTimeout(timer);
    timer = setTimeout(fn, delay);
    };
}
