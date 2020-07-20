$(function () {
    $("#Enviar").click(
        function () {
            var txtMensagem = $("#mensagem");
            var url = "api/Chat";

            $.ajax({
                type: "POST",
                url: url,
                async: false,
                data: { mensagem: txtMensagem.val() },
                success: function (data) {
                    var display = $("#display-mensagem");

                    display
                        .append(" >> EU: " + txtMensagem.val() + "\n")
                        .append(" >> BOOT: " + data.resposta + "\n");

                    txtMensagem.val("");
                }
            });
        }
    );
});