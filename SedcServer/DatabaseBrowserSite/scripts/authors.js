$(() => {
    const alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".split("");

    for (const letter of alphabet) {
        const button = $("<button>");
        button.text(letter);
        button.on('click', async () => {
            $("#body").empty();
            const authors = await loadDataFiltered("Authors", 'Name', letter);
            for (const author of authors) {
                $("#body").append(`<tr>
                    <td>${author.ID}</td>
                    <td>${author.Name}</td>
                </tr>`);
            }
        });
        button.appendTo("#buttons");
    }

});