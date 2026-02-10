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
                <td>${getStatusBadge(item.status ?? item.statusId)}</td>
                <td>${item.startDate?.substring(0, 10)}</td>
                <td class="text-end">
                    <div class="d-inline-flex gap-2">
                        <a class="btn btn-sm btn-outline-success" href="/Admin/Activities/ActivityDetails/${item.id}"><i class="bi bi-eye"></i></a>
                        <a class="btn btn-sm btn-outline-primary" href="/Admin/Activities/EditActivity/${item.id}"><i class="bi bi-pencil"></i></a>
                        <a class="btn btn-sm btn-outline-info"><i class="bi bi-people" href="?handler=participants&activityId=${item.id}" onclick="return loadParticipants(event, ${item.id})">></i></a>
                        <a class="btn btn-sm btn-outline-warning"><i class="bi bi-check-circle"></i></a>
                        <a class="btn btn-sm btn-outline-danger"><i class="bi bi-trash"></i></a>
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
function loadParticipants(e, activityId) {
    e.preventDefault();

    fetch(`${window.participantsUrl}&activityId=${activityId}`)
        .then(res => res.json())
        .then(data => {

            console.log("DATA:", data);

            fillParticipants("coordinatorsList", data.coordinators);
            fillParticipants("supervisorsList", data.supervisors);
            fillParticipants("viewersList", data.viewer);

            const modalEl = document.getElementById("participantsModal");
            const modal = new bootstrap.Modal(modalEl);
            modal.show();
        })
        .catch(err => console.error(err));

    return false;
}


function fillParticipants(listId, items) {

    const list = document.getElementById(listId);
    if (!list) return;

    list.innerHTML = "";

    if (!items || items.length === 0) {
        list.innerHTML = `
            <li class="list-group-item text-muted">
                No participants
            </li>`;
        return;
    }

    items.forEach(p => {

        const fullName = p.fullname ?? p.fullName ?? '';
        const userName = p.username ?? p.useraname ?? p.userName ?? '';
        const participantId = p.id;

        list.innerHTML += `
            <li class="list-group-item d-flex justify-content-between align-items-center">
                <div>
                    <div class="fw-semibold">${fullName}</div>
                    <div class="text-muted small">${userName}</div>
                </div>

                <a href="?handler=Certificate&participantId=${participantId}"
                   class="btn btn-sm btn-outline-success"
                   title="View Certificate"
                   onclick="return loadCertificate(event, ${participantId})">
                    <i class="bi bi-patch-check-fill"></i>
                </a>
            </li>`;
    });
}
function getStatusBadge(status) {

    switch (status) {
        case 1:
            return `<span class="badge bg-success">In Progress</span>`;
        case 2:
            return `<span class="badge bg-primary">Completed</span>`;
        case 3:
            return `<span class="badge bg-danger">Upcoming</span>`;

    }
}









