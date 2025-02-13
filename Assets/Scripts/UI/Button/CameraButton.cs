using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace HardCoded.VRigUnity {
	public class CameraButton : MonoBehaviour {
		private TMP_Text text;
		private Button toggleButton;
		private Image buttonImage;
		private bool isCameraShowing;

		[SerializeField] Color toggleOnColor  = new(0.08009967f, 0.6792453f, 0.3454931f); // 0x14AD58
		[SerializeField] Color toggleOffColor = new(0.6981132f, 0, 0.03523935f); // 0xB30009

		void Start() {
			text = GetComponentInChildren<TMP_Text>();
			buttonImage = GetComponent<Image>();
			toggleButton = GetComponent<Button>();

			InitializeContents();
			SolutionUtils.GetSolution().SetErrorListener(error => {
				SetCamera(false);
			});
		}

		private void InitializeContents() {
			buttonImage.color = toggleOnColor;
			isCameraShowing = false;

			toggleButton.onClick.RemoveAllListeners();
			toggleButton.onClick.AddListener(delegate {
				SetCamera(!isCameraShowing);
			});
		}

		private void SetCamera(bool enable) {
			isCameraShowing = enable;
			buttonImage.color = enable ? toggleOffColor : toggleOnColor;
			text.text = enable ? "Stop Camera" : "Start Camera";
			
			if (enable) {
				SolutionUtils.GetSolution().Play();
			} else {
				SolutionUtils.GetSolution().ResetVRMAnimator();
				SolutionUtils.GetSolution().Stop();
			}
		}
	}
}
