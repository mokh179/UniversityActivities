function searchActivities(pageNumber, formElement) {

    if (!formElement) {
        console.error("Form element is null");
        return;
    }
    console.log(document.getElementById("searchForm"));

    const form = document.getElementById("searchForm");
    const formData = new FormData(form);
    formData.append("pageNumber", pageNumber);

    const query = new URLSearchParams(formData).toString();
    console.log(query);
    fetch(`?handler=Filter&${query}`)
        .then(res => res.json())
        .then(data => {
            rebuildTable(data);
            rebuildPagination(data);
            rebuildInfo(data);
        });
}


function rebuildTable(result) {
    console.log("table");
    const tbody = document.getElementById("activitiesTableBody");
    tbody.innerHTML = "";
    console.log(result)
    // 👇 تأكيد إن items موجودة
    if (!result.items || result.items.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="7" class="text-center text-muted py-4">
                    No data found
                </td>
            </tr>`;
        return;
    }

    result.items.forEach((item, i) => {

        tbody.innerHTML += `
            <tr>
                <td>${((result.pageNumber - 1) * result.pageSize) + i + 1}</td>
                <td>${item.titleEn ?? item.titleEn}</td>
                <td>${item.managementNameEn}</td>
                <td>
                    <span class="badge bg-success">
                        ${item.statusText ?? item.status}
                    </span>
                </td>
                <td>${item.startDate?.substring(0, 10)}</td>
                <td class="text-end">
                    <div class="d-inline-flex gap-2">
                        <button class="btn btn-sm btn-outline-success"><i class="bi bi-eye"></i></button>
                        <button class="btn btn-sm btn-outline-primary"><i class="bi bi-pencil"></i></button>
                        <button class="btn btn-sm btn-outline-info"><i class="bi bi-people"></i></button>
                        <button class="btn btn-sm btn-outline-warning"><i class="bi bi-check-circle"></i></button>
                        <button class="btn btn-sm btn-outline-danger"><i class="bi bi-trash"></i></button>
                    </div>
                </td>
            </tr>`;
    });
}

        function rebuildPagination(result) {
        const ul = document.getElementById("paginationContainer");
        ul.innerHTML = "";

        for (let i = 1; i <= result.totalPages; i++) {
            ul.innerHTML += `
                <li class="page-item ${i === result.pageNumber ? "active" : ""}">
                    <a href="javascript:void(0)"
                       class="page-link ${i === result.pageNumber ? "bg-success border-success" : "text-success"}"
                       onclick="searchActivities(${i})">
                        ${i}
                    </a>
                </li>`;
        }
    }


    function rebuildInfo(result) {

        const info = document.getElementById("paginationInfo");

        if (!result || result.totalCount === 0) {
            info.innerText = "Showing 0 to 0 of 0 entries";
            return;
        }

        const from = ((result.pageNumber - 1) * result.pageSize) + 1;
        const to = Math.min(result.pageNumber * result.pageSize, result.totalCount);

        info.innerText = `Showing ${from} to ${to} of ${result.totalCount} entries`;
    }

    function clearAllFilters() {

        document.getElementById("searchForm").reset();

        document.getElementById("managementContainer").classList.add("d-none");
        document.getElementById("clubContainer").classList.add("d-none");

        document.getElementById("managementSelect").innerHTML =
            `<option value="">All Managements</option>`;
        document.getElementById("clubSelect").innerHTML =
            `<option value="">All Clubs</option>`;

        loadDefaultActivities(1);
    }

        function loadManagements() {

        const organizationId = document.getElementById("managementtypeSelect").value;
        const managementContainer = document.getElementById("managementContainer");
        const managementSelect = document.getElementById("managementSelect");

        // Reset management
        managementSelect.innerHTML =
            `<option value="">All Managements</option>`;
        managementContainer.classList.add("d-none");

        // Reset & hide club
        document.getElementById("clubSelect").innerHTML =
            `<option value="">All Clubs</option>`;
        document.getElementById("clubContainer")
            .classList.add("d-none");

        if (!organizationId)
            return;

            fetch(`?handler=ManagementsByType&managementTypeId=${organizationId}`)
            .then(res => res.json())
            .then(data => {

                if (!data || data.length === 0)
                    return;

                data.forEach(item => {
                    managementSelect.innerHTML +=
                        `<option value="${item.id}">${item.nameEn}</option>`;
                });

                managementContainer.classList.remove("d-none");
            });
    }


    function loadClubs() {

        const managementId = document.getElementById("managementSelect").value;
        const clubContainer = document.getElementById("clubContainer");
        const clubSelect = document.getElementById("clubSelect");
        console.log(managementId);
        // Reset & hide club
        clubSelect.innerHTML =
            `<option value="">All Clubs</option>`;
        clubContainer.classList.add("d-none");

        if (!managementId)
            return;

        fetch(`?handler=ClubsByManagement&managementId=${managementId}`)
            .then(res => res.json())
            .then(data => {

                // ❌ مفيش داتا
                if (!data || data.length === 0)
                    return;

                // ✔️ في داتا
                data.forEach(item => {
                    clubSelect.innerHTML +=
                        `<option value="${item.id}">${item.name}</option>`;
                });

                clubContainer.classList.remove("d-none");
            });
}

function loadDefaultActivities(pageNumber) {

    fetch(`?handler=Default&pageNumber=${pageNumber}`)
        .then(res => res.json())
        .then(data => {
            rebuildTable(data);
            rebuildPagination(data);
            rebuildInfo(data);
        });
}

