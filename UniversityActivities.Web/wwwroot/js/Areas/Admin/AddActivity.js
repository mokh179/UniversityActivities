


console.log("Welcome In js");

document.getElementById("activityImage")
    .addEventListener("change", function (e) {

        const file = e.target.files[0];
        if (!file) return;

        const reader = new FileReader();
        reader.onload = function () {

            const preview = document.getElementById("imagePreview");
            preview.src = reader.result;
            preview.classList.remove("d-none");

            document.getElementById("uploadPlaceholder")
                .classList.add("d-none");
        };

        reader.readAsDataURL(file);
    });





const managementTypeSelect = document.getElementById("managementTypeSelect");
const managementSelect = document.getElementById("managementSelect");
const clubSelect = document.getElementById("clubSelect");

const managementWrapper = document.getElementById("managementFieldWrapper");
const clubWrapper = document.getElementById("clubFieldWrapper");

managementTypeSelect.addEventListener("change", async function () {

    const typeId = this.value;

    // reset
    managementSelect.innerHTML = '<option value="">Select Management</option>';
    clubSelect.innerHTML = '<option value="">Select Club</option>';
    managementWrapper.classList.add("d-none");
    clubWrapper.classList.add("d-none");

    if (!typeId) return;

    const response = await fetch(
        `/Admin/Activities/AddActivity?handler=ManagementsByType&managementTypeId=${typeId}`
    );

    const data = await response.json();

    if (!data || data.length === 0) return;

    data.forEach(item => {
        const option = document.createElement("option");
        option.value = item.id;
        option.textContent = item.nameEn;
        managementSelect.appendChild(option);
    });

    managementWrapper.classList.remove("d-none");
});

// 2️⃣ Management → Club
managementSelect.addEventListener("change", async function () {

    const managementId = this.value;

    clubSelect.innerHTML = '<option value="">Select Club</option>';
    clubWrapper.classList.add("d-none");

    if (!managementId) return;

    const response = await fetch(
        `/Admin/Activities/AddActivity?handler=ClubsByManagement&managementId=${managementId}`
    );

    const data = await response.json();

    if (!data || data.length === 0) return;

    data.forEach(item => {
        const option = document.createElement("option");
        option.value = item.id;
        option.textContent = item.nameEn;
        clubSelect.appendChild(option);
    });

    clubWrapper.classList.remove("d-none");
});

const attendanceRadios = document.querySelectorAll('input[name="AttendanceType"]');
const locationWrapper = document.getElementById("locationWrapper");
const linkWrapper = document.getElementById("linkWrapper");

function toggleAttendanceFields(value) {
    if (value === "2") {
        linkWrapper.classList.remove("d-none");
        locationWrapper.classList.add("d-none");
    } else {
        locationWrapper.classList.remove("d-none");
        linkWrapper.classList.add("d-none");
    }
}

attendanceRadios.forEach(radio => {
    radio.addEventListener("change", e => {
        toggleAttendanceFields(e.target.value);
    });
});

// Init (default)
toggleAttendanceFields(
    document.querySelector('input[name="AttendanceType"]:checked').value
);
    /* =====================================================
    GLOBAL ASSIGNMENT STATE (FINAL SHAPE)
    ===================================================== */

    window.assignment = {
        coordinators: [],
    supervisors: [],
    viewers: []
    };

    const pageUrl = '/Admin/Activities/AddActivity';

    /* =====================================================
       UPDATE UI (ONE ENTRY POINT)
    ===================================================== */
    function updateUI() {
        renderChips();
    renderAllDropdowns();
    }

    /* =====================================================
       INIT CHIP DROPDOWN
    ===================================================== */
    function initChipDropdown(wrapperId, fetchUrl) {

        const wrapper = document.getElementById(wrapperId);
    if (!wrapper) return;

    const select = wrapper.querySelector(".chip-select");
    const input = select.querySelector(".chip-input");
    const dropdown = select.querySelector(".chip-dropdown");

    // prevent re-init
    if (select._initialized) return;
    select._initialized = true;

    fetch(fetchUrl)
            .then(r => r.json())
            .then(data => {
        select._cachedItems = data || [];
    updateUI();
            });

    input.addEventListener("input", updateUI);
    }

    /* =====================================================
       RENDER ALL DROPDOWNS
    ===================================================== */
    function renderAllDropdowns() {

        document.querySelectorAll(".chip-select").forEach(select => {

            const dropdown = select.querySelector(".chip-dropdown");
            const input = select.querySelector(".chip-input");
            const items = select._cachedItems || [];

            dropdown.innerHTML = "";

            const roleId = parseInt(select.dataset.roleId); // 1 | 2 | 3
            const roleName = select.dataset.roleName;
            const q = input.value.toLowerCase();

            const targetList =
                roleId === 1 ? assignment.coordinators :
                    roleId === 2 ? assignment.supervisors :
                        assignment.viewers;

            items.forEach(item => {

                const displayName =
                    item.fullname || item.name || item.nameEn || item.userName || "";

                if (q && !displayName.toLowerCase().includes(q)) return;

                // hide if already selected in same role
                if (targetList.some(x => x.userId === item.id)) return;

                const div = document.createElement("div");
                div.className = "chip-item";

                div.innerHTML = `
                    <div class="item-text">
                        <strong>${displayName}</strong>
                    </div>
                    <span class="item-badge">${roleName}</span>
                `;

                div.onclick = () => {

                    targetList.push({
                        id: 0,
                        userId: item.id,
                        userName: displayName,
                        roleId: roleId,
                        activeId: 0
                    });

                    input.value = "";
                    updateUI();
                };

                dropdown.appendChild(div);
            });

            dropdown.style.display = "block";
        });
    }

    /* =====================================================
       REMOVE ASSIGNMENT ITEM
    ===================================================== */
    function removeAssignment(userId, roleId) {

        if (roleId === 1)
    assignment.coordinators =
                assignment.coordinators.filter(x => x.userId !== userId);

    if (roleId === 2)
    assignment.supervisors =
                assignment.supervisors.filter(x => x.userId !== userId);

    if (roleId === 3)
    assignment.viewers =
                assignment.viewers.filter(x => x.userId !== userId);

    updateUI();
    }

    /* =====================================================
       RENDER CHIPS (NO FORM / NO INPUTS)
    ===================================================== */
    function renderChips() {

        const panel = document.getElementById("selectedChipsPanel");
    panel.innerHTML = "";

        const renderGroup = (title, list, roleId) => {

            if (list.length === 0) return;

    panel.insertAdjacentHTML(
    "beforeend",
    `<h6 class="mt-3">${title}</h6>`
    );

            list.forEach(item => {

                const chip = document.createElement("div");
    chip.className = "chip";
    chip.innerHTML = `
    ${item.userName}
    <span class="chip-remove">&times;</span>
    `;

    chip.querySelector(".chip-remove").onclick =
                    () => removeAssignment(item.userId, roleId);

    panel.appendChild(chip);
            });
        };

    renderGroup("Coordinators", assignment.coordinators, 1);
    renderGroup("Supervisors", assignment.supervisors, 2);
    renderGroup("Viewers", assignment.viewers, 3);
    }

    /* =====================================================
       GET FINAL OBJECT (FOR AJAX / DEBUG)
    ===================================================== */
    function getAssignmentPayload() {
        return {
        assignment: assignment
        };
    }

    /* =====================================================
       MANAGEMENT CHANGE → INIT DROPDOWNS
    ===================================================== */
    document.addEventListener("DOMContentLoaded", function () {

  
        
        const managementSelect = document.getElementById("managementSelect");

    managementSelect.addEventListener("change", function () {

            const managementId = this.value;
    if (!managementId) return;

    const url =
    `${pageUrl}?handler=UsersByManagement&managementId=${managementId}`;

    ["dropdown1Wrapper", "dropdown2Wrapper", "dropdown3Wrapper"]
                .forEach(id => {
        document.getElementById(id).classList.remove("d-none");
    initChipDropdown(id, url);
                });
        });
    });


    function flattenAssignments() {

        return [
            ...assignment.coordinators.map(x => ({
        userId: x.userId,
    activityRoleId: x.roleId
            })),

            ...assignment.supervisors.map(x => ({
        userId: x.userId,
    activityRoleId: x.roleId
            })),

            ...assignment.viewers.map(x => ({
        userId: x.userId,
    activityRoleId: x.roleId
            }))
    ];
    }

    function buildCreateActivityPayload() {

        const payload = {

            // ===== Basic Info =====
    imageUrl:"",
    titleEn: document.querySelector('[name="Input.TitleEn"]')?.value || "",
    titleAr: document.querySelector('[name="Input.TitleAr"]')?.value || "",
            startDate:
                document.getElementById("startDateTime")?.value || null,

            endDate:
                document.getElementById("endDateTime")?.value || null,
    attendanceScopeId:parseInt(
            document.querySelector('[name="Input.AttendanceScopeId"]:checked').value
        ),

    //managementTypeId:
    //document.getElementById("managementTypeSelect")?.value || null,

            managementId:parseInt(document.getElementById("managementSelect")?.value) || null,

    clubId:
    parseInt(document.getElementById("clubSelect")?.value) || null,

    activityTypeId:
    parseInt(document.getElementById("activityField")?.value) || null,

    descriptionEn:
    document.querySelector('[name="Input.DescriptionEn"]')?.value || "",

    descriptionAr:
    document.querySelector('[name="Input.DescriptionAr"]')?.value || "",

    attendanceModeId:
    parseInt(document.querySelector('[name="AttendanceType"]:checked')?.value) || null,

    locationEn:
    document.getElementById("locationWrapper")?.classList.contains("d-none")
    ? null
    : document.querySelector('[name="Input.LocationEn"]')?.value || null,

    onlineLink:
    document.getElementById("linkWrapper")?.classList.contains("d-none")
    ? null
    : document.querySelector('[name="Input.OnlineLink"]')?.value || null,

    activityCode:
    document.querySelector('[name="Input.ActivityCode"]')?.value || null,

    targetAudienceIds: Array.from(
    document.querySelectorAll('input[name="Input.TargetAudienceIds"]:checked')
            ).map(x => parseInt(x.value)),

    // ✅ Assignments (FINAL SHAPE)
    assignments: flattenAssignments(),
    isPublished:
    document.getElementById("publishCheckbox")?.checked ?? false
        };

    return payload;
    }


    document.querySelector("#createActivityBtn")
    .addEventListener("click", async function (e) {

        e.preventDefault();

    const activityDto = buildCreateActivityPayload();

    const formData = new FormData();

    // 👈 1) JSON DTO
    formData.append(
    "activityJson",
    JSON.stringify(activityDto)
    );

    // 👈 2) Image file
    const imageInput = document.getElementById("activityImage");
         if (imageInput && imageInput.files.length > 0) {
        formData.append(
            "image",
            imageInput.files[0]
        );
         }

    try {
             const res = await fetch(
    "/Admin/Activities/AddActivity?handler=Create",
    {
        method: "POST",
    body: formData // ❌ لا headers
                 }
    );

    if (!res.ok) {
        alert("Something went wrong");
    return;
             }

    const result = await res.json();

    if (result === true || result?.success === true) {
        sessionStorage.setItem(
            "activitySuccessMessage",
            "Activity added successfully"
        );
    window.location.href = "/Admin/Activities/Index";
             }

         } catch (err) {
        console.error(err);
    alert("Unexpected error");
         }
     });





