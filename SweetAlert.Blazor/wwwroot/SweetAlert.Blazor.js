
// functions, and may import other JavaScript modules if required.

export function loadSweetAlert() {
    const existingScript = document.getElementById('sweetAlert');

    if (!existingScript) {
        const script = document.createElement('script');
        script.src = '_content/SweetAlert.Blazor/lib/sweetalert.min.js';
        script.id = 'sweetAlert';
        document.body.appendChild(script);

    }
}

export function showAlert(title,message,type) {
  return prompt(message, 'Type anything here');
}
export function showConfirm(title, message,confirmText,cancelText, type, onconfirm) {

}
export function showAlertComplex(swalOptions) {
    return Swal.fire(swalOptions);
}
