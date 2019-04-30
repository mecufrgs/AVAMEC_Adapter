function saveDataOnLocalStorage(){
    /*Quebra a URL em um vetor.*/ 
    let url = window.location.href.split("/")
    /*Por padrão a página é a última informação na URL.*/ 
    let page = url[url.length - 1]
    /*Por padrão o número da página é dado após o underline.*/
    let pageNumber = page.split('.')[0].split('_')[1]
    /*Por padrão o nome da unidade é o diretório onde estão as páginas html.*/
    let unit = url[url.length - 2]

    localStorage.setItem("op", pageNumber)
    localStorage.setItem("ou", unit)
}

saveDataOnLocalStorage()
window.location = "../../index.html"

