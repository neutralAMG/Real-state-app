document.getElementById('agentSearch').addEventListener('input', function () {
    var searchValue = this.value.toLowerCase();
    var agents = document.querySelectorAll('.agent');

    agents.forEach(function (agent) {
        var agentName = agent.querySelector('h2').textContent.toLowerCase();
        if (agentName.includes(searchValue)) {
            agent.style.display = '';
        } else {
            agent.style.display = 'none';
        }
    });
});