


console.log("🔵 Welcome In js - AddActivity.js loaded");
console.log("🔵 Current window.assignment:", window.assignment);

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

    if (!window.assignment) {
        window.assignment = {
            coordinators: [],
            supervisors: [],
            viewers: []
        };
        console.log("🔵 Initialized window.assignment:", window.assignment);
    } else {
        console.log("🔵 window.assignment already exists:", window.assignment);
    }

    const pageUrl = '/Admin/Activities/AddActivity';

    /* =====================================================
       UPDATE UI (ONE ENTRY POINT)
    ===================================================== */
    function updateUI() {
        console.log(`\n🟡 updateUI called`);
        renderChips();
    renderAllDropdowns();
        console.log(`✅ updateUI completed\n`);
    }

    /* =====================================================
       INIT CHIP DROPDOWN
    ===================================================== */
    function initChipDropdown(wrapperId, fetchUrl) {
        console.log(`\n🔵 initChipDropdown called - wrapperId: ${wrapperId}, fetchUrl: ${fetchUrl}`);

        const wrapper = document.getElementById(wrapperId);
        console.log(`  - Wrapper element:`, wrapper);
        
    if (!wrapper) {
            console.error(`  ❌ Wrapper not found for id: ${wrapperId}`);
            return;
        }

    const select = wrapper.querySelector(".chip-select");
        console.log(`  - Chip-select element:`, select);
        
    if (!select) {
            console.error(`  ❌ .chip-select not found inside wrapper ${wrapperId}`);
            return;
        }
        
    const input = select.querySelector(".chip-input");
        console.log(`  - Chip-input element:`, input);
        
    if (!input) {
            console.error(`  ❌ .chip-input not found inside chip-select`);
            return;
        }
        
    const dropdown = select.querySelector(".chip-dropdown");
        console.log(`  - Chip-dropdown element:`, dropdown);
        
    if (!dropdown) {
            console.error(`  ❌ .chip-dropdown not found inside chip-select`);
            return;
        }

    // prevent re-init
    if (select._initialized) {
            console.log(`  ⚠️ Already initialized, skipping`);
            return;
        }
        
    select._initialized = true;
        console.log(`  ✅ Marked as initialized`);

        console.log(`  - Fetching data from: ${fetchUrl}`);
    fetch(fetchUrl)
            .then(r => {
                console.log(`  - Fetch response status:`, r.status);
                return r.json();
            })
            .then(data => {
                console.log(`  - Fetched data:`, data);
                console.log(`  - Data length:`, data?.length || 0);
                
        select._cachedItems = data || [];
                console.log(`  - Cached items count:`, select._cachedItems.length);
                
        updateUI();
                console.log(`  ✅ updateUI called`);
            })
            .catch(error => {
                console.error(`  ❌ Fetch error:`, error);
            });

    input.addEventListener("input", updateUI);
        console.log(`  ✅ Input event listener added`);
    }

    /* =====================================================
       RENDER ALL DROPDOWNS
    ===================================================== */
    function renderAllDropdowns() {
        console.log(`\n🔵 renderAllDropdowns called`);

        const chipSelects = document.querySelectorAll(".chip-select");
        console.log(`  - Found ${chipSelects.length} .chip-select elements`);
        
        if (chipSelects.length === 0) {
            console.warn(`  ⚠️ No .chip-select elements found in the page!`);
        }

        chipSelects.forEach((select, index) => {
            console.log(`\n  📦 Processing chip-select ${index + 1}:`, select);

            const dropdown = select.querySelector(".chip-dropdown");
            const input = select.querySelector(".chip-input");
            
            if (!dropdown) {
                console.error(`    ❌ .chip-dropdown not found`);
                return;
            }
            
            if (!input) {
                console.error(`    ❌ .chip-input not found`);
                return;
            }
            
            const items = select._cachedItems || [];
            console.log(`    - Cached items:`, items.length);

            dropdown.innerHTML = "";

            const roleId = parseInt(select.dataset.roleId); // 1 | 2 | 3
            const roleName = select.dataset.roleName;
            const q = input.value.toLowerCase();
            
            console.log(`    - roleId: ${roleId}, roleName: ${roleName}, search query: "${q}"`);

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
                    console.log(`\n🟢 Chip item clicked - Adding user to role ${roleId}`);
                    console.log(`  - User ID: ${item.id}`);
                    console.log(`  - Display Name: ${displayName}`);
                    console.log(`  - Target list before:`, [...targetList]);

                    targetList.push({
                        id: 0,
                        userId: item.id,
                        userName: displayName,
                        roleId: roleId,
                        activeId: 0
                    });

                    console.log(`  - Target list after:`, [...targetList]);
                    console.log(`  - Full assignment object:`, assignment);

                    input.value = "";
                    updateUI();
                };

                dropdown.appendChild(div);
            });

            dropdown.style.display = "block";
            console.log(`    ✅ Rendered ${dropdown.children.length} items in dropdown`);
        });
        
        console.log(`\n✅ renderAllDropdowns completed`);
    }

    /* =====================================================
       REMOVE ASSIGNMENT ITEM
    ===================================================== */
    function removeAssignment(userId, roleId) {
        console.log(`\n🔴 removeAssignment called - userId: ${userId}, roleId: ${roleId}`);

        if (roleId === 1) {
            console.log(`  - Removing from coordinators`);
            console.log(`  - Before:`, [...assignment.coordinators]);
    assignment.coordinators =
                assignment.coordinators.filter(x => x.userId !== userId);
            console.log(`  - After:`, [...assignment.coordinators]);
        }

    if (roleId === 2) {
            console.log(`  - Removing from supervisors`);
            console.log(`  - Before:`, [...assignment.supervisors]);
    assignment.supervisors =
                assignment.supervisors.filter(x => x.userId !== userId);
            console.log(`  - After:`, [...assignment.supervisors]);
        }

    if (roleId === 3) {
            console.log(`  - Removing from viewers`);
            console.log(`  - Before:`, [...assignment.viewers]);
    assignment.viewers =
                assignment.viewers.filter(x => x.userId !== userId);
            console.log(`  - After:`, [...assignment.viewers]);
        }

    updateUI();
    }

    /* =====================================================
       RENDER CHIPS (NO FORM / NO INPUTS)
    ===================================================== */
    function renderChips() {
        console.log(`\n🔵 renderChips called`);
        console.log(`  - assignment.coordinators:`, assignment.coordinators);
        console.log(`  - assignment.supervisors:`, assignment.supervisors);
        console.log(`  - assignment.viewers:`, assignment.viewers);

        const panel = document.getElementById("selectedChipsPanel");
        console.log(`  - selectedChipsPanel element:`, panel);
        
    if (!panel) {
            console.error(`  ❌ selectedChipsPanel not found!`);
            return;
        }
        
    panel.innerHTML = "";

        const renderGroup = (title, list, roleId) => {
            console.log(`    📋 renderGroup - title: ${title}, list length: ${list.length}, roleId: ${roleId}`);

            if (list.length === 0) {
                console.log(`    ⚠️ List is empty, skipping ${title}`);
                return;
            }

    panel.insertAdjacentHTML(
    "beforeend",
    `<h6 class="mt-3">${title}</h6>`
    );
            console.log(`    ✅ Added title "${title}" to panel`);

            list.forEach((item, index) => {
                console.log(`      🏷️ Creating chip ${index + 1} for user: ${item.userName} (userId: ${item.userId})`);

                const chip = document.createElement("div");
    chip.className = "chip";
    chip.innerHTML = `
    ${item.userName}
    <span class="chip-remove">&times;</span>
    `;

    chip.querySelector(".chip-remove").onclick =
                    () => removeAssignment(item.userId, roleId);

    panel.appendChild(chip);
                console.log(`      ✅ Chip added to panel`);
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
       NOTE: This code is handled by the inline script in AddActivity.cshtml
       which uses the multi-select system. This section is kept for reference
       but the actual implementation is in the page's inline script.
    ===================================================== */
    document.addEventListener("DOMContentLoaded", function () {
        console.log("🔵 DOMContentLoaded - AddActivity.js loaded");
        console.log("🔵 Note: Management select handling is done by inline script in AddActivity.cshtml");
        console.log("🔵 Current window.assignment:", window.assignment);
        
        // The multi-select system in AddActivity.cshtml handles:
        // - Loading users when management is selected
        // - Populating menu-coordinators, menu-supervisors, menu-viewers
        // - Updating window.assignment when users are selected/deselected
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





