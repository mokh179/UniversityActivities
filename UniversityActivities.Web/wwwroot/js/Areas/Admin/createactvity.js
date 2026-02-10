/* =====================================================
   GLOBAL DATA
   ===================================================== */

let allUsers = [];

const assignmentsState = {
    1: [], // coordinators
    2: [], // reviewers
    3: []  // participants
};

/* =====================================================
   HELPERS
   ===================================================== */

async function fillSelect(select, url, text) {
    select.innerHTML = `<option>Loading...</option>`;
    const res = await fetch(url);
    const data = await res.json();
    select.innerHTML = `<option value="">${text}</option>`;
    data.forEach(x => select.append(new Option(x.nameEn, x.id)));
}

/* =====================================================
   MANAGEMENT
   ===================================================== */

managementTypeSelect.onchange = async e => {
    await fillSelect(
        managementSelect,
        `?handler=ManagementsByType&managementTypeId=${e.target.value}`,
        "Select Management"
    );
    managementSelect.dispatchEvent(new Event("change"));
};

managementSelect.onchange = async e => {
    await fillSelect(
        clubSelect,
        `?handler=ClubsByManagement&managementId=${e.target.value}`,
        "Select Club"
    );

    const res = await fetch(
        `?handler=UsersByManagement&managementId=${e.target.value}`
    );
    allUsers = await res.json();

    document.querySelectorAll(".chips-dropdown").forEach(initRole);
};

/* =====================================================
   ROLES (CHIPS)
   ===================================================== */

function initRole(wrapper) {

    const role = wrapper.dataset.role;

    const roleId =
        role === "coordinators" ? 1 :
            role === "reviewers" ? 2 :
                role === "participants" ? 3 : 0;
    console.log("ROLE KEY FROM DOM =", role);

    const chips = wrapper.querySelector(".chips");
    const list = wrapper.querySelector(".dropdown-list");
    const input = wrapper.querySelector(".role-search");

    // ✅ IMPORTANT: sync with global state
    const selected = new Set(assignmentsState[roleId]);

    function render(q = "") {
        list.innerHTML = "";

        allUsers
            .filter(u => !selected.has(u.id))
            .filter(u =>
                u.name.toLowerCase().includes(q) ||
                u.username.toLowerCase().includes(q)
            )
            .slice(0, 10)
            .forEach(u => {
                const li = document.createElement("li");
                li.textContent = `${u.name} (${u.username})`;
                li.onclick = () => selectUser(u);
                list.appendChild(li);
            });
    }

    function selectUser(u) {
        if (selected.has(u.id)) return;

        selected.add(u.id);

        if (!assignmentsState[roleId].includes(u.id)) {
            assignmentsState[roleId].push(u.id);
        }

        renderChips();
        render("");
        input.value = "";
    }

    function renderChips() {
        chips.innerHTML = "";

        selected.forEach(id => {
            const u = allUsers.find(x => x.id === id);
            if (!u) return;

            const chip = document.createElement("div");
            chip.className = "chip";
            chip.innerHTML = `
                ${u.name}
                <button type="button">&times;</button>
            `;

            chip.querySelector("button").onclick = () => {
                selected.delete(id);

                assignmentsState[roleId] =
                    assignmentsState[roleId].filter(x => x !== id);

                renderChips();
                render(input.value.toLowerCase());
            };

            chips.appendChild(chip);
        });
    }

    input.oninput = e => render(e.target.value.toLowerCase());

    renderChips();
    render();
}

/* =====================================================
   SUBMIT → SEND JSON
   ===================================================== */

document.getElementById("createActivityForm")
    .addEventListener("submit", function () {

        const result = [];
        console.log("AssignmentsState:", assignmentsState);
        Object.keys(assignmentsState).forEach(roleId => {
            if (assignmentsState[roleId].length === 0) return;

            result.push({
                roleId: parseInt(roleId),
                activityId: 0,
                userIds: assignmentsState[roleId]
            });
        });
        console.log("Res:", result);

        document.getElementById("AssignmentsJson").value =
            JSON.stringify(result);

        console.log("FINAL AssignmentsJson:", result);
    });
