
// functions, and may import other JavaScript modules if required.

export function loadSweetAlert() {
    const existingScript = document.getElementById('sweetAlert');

    if (!existingScript) {
        const script = document.createElement('script');
        script.src = '_content/SweetAlert.Blazor/lib/sweetalert2.js';
        script.id = 'sweetAlert';
        document.body.appendChild(script);

    }
}

export async function showAlert(title, message, severity) {
    return await Swal.fire(title,message,severity);
}
export async function showConfirm(title, message,severity, confirmText, cancelText, confirmClass , cancelClass , dangerMode) {
    let confirm = false
    let result = await Swal.fire({
        title: title,
        text: message,
        icon: severity,
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: confirmText,
        confirmButtonColor: dangerMode ?'#DC3545':'',
        cancelButtonText: cancelText,
        customClass: {
            cancelButton: cancelClass,
            confirmButton:confirmClass
        }       
    })
    if (result) {
        confirm = result.isConfirmed
    }
    return confirm
}
export function showAlertComplex(swalOptions) {
    return Swal.fire(swalOptions);
}
