﻿using System;
using UnityEngine;

namespace HardCoded.VRigUnity {
	// TODO: Optimize this class
	public class BoneSettings {
		public const int FACE = 0;
		public const int NECK = 1;
		public const int LEFT_ARM = 2;
		public const int LEFT_WRIST = 3;
		public const int LEFT_FINGERS = 4;
		public const int RIGHT_ARM = 5;
		public const int RIGHT_WRIST = 6;
		public const int RIGHT_FINGERS = 7;
		public const int CHEST = 8;
		public const int HIPS = 9;
		public const int LEFT_HIP = 10;
		public const int LEFT_KNEE = 11;
		public const int LEFT_ANKLE = 12;
		public const int RIGHT_HIP = 13;
		public const int RIGHT_KNEE = 14;
		public const int RIGHT_ANKLE = 15;
		public const int Count = RIGHT_ANKLE;

		// 16 bits set
		public const int Default = ((1 << (Count + 2)) - 1) & ~(
			(1 << LEFT_HIP) | (1 << LEFT_KNEE) | (1 << LEFT_ANKLE) |
			(1 << RIGHT_HIP) | (1 << RIGHT_KNEE) | (1 << RIGHT_ANKLE)
		);

		public static bool Get(int index) {
			return (Settings.BoneMask & (1 << index)) != 0;
		}

		public static void Set(int index, bool set) {
			if (set) {
				Settings.BoneMask |= 1 << index;
			} else {
				Settings.BoneMask &= ~(1 << index);
			}
		}
		
		private static readonly HumanBodyBones[] FACE_BONES = new [] {
			HumanBodyBones.LeftEye,
			HumanBodyBones.RightEye
		};

		private static readonly HumanBodyBones[] NECK_BONES = new [] {
			HumanBodyBones.Neck,
			HumanBodyBones.Head,
			HumanBodyBones.Jaw
		};

		private static readonly HumanBodyBones[] LEFT_ARM_BONES = new [] {
			HumanBodyBones.LeftUpperArm,
			HumanBodyBones.LeftLowerArm
		};
		private static readonly HumanBodyBones[] LEFT_WRIST_BONES = new [] { HumanBodyBones.LeftHand };
		private static readonly HumanBodyBones[] LEFT_FINGERS_BONES = new [] {
			HumanBodyBones.LeftIndexProximal,
			HumanBodyBones.LeftIndexIntermediate,
			HumanBodyBones.LeftIndexDistal,
			HumanBodyBones.LeftMiddleProximal,
			HumanBodyBones.LeftMiddleIntermediate,
			HumanBodyBones.LeftMiddleDistal,
			HumanBodyBones.LeftRingProximal,
			HumanBodyBones.LeftRingIntermediate,
			HumanBodyBones.LeftRingDistal,
			HumanBodyBones.LeftLittleProximal,
			HumanBodyBones.LeftLittleIntermediate,
			HumanBodyBones.LeftLittleDistal,
			HumanBodyBones.LeftThumbProximal,
			HumanBodyBones.LeftThumbIntermediate,
			HumanBodyBones.LeftThumbDistal
		};
		
		private static readonly HumanBodyBones[] RIGHT_ARM_BONES = new [] {
			HumanBodyBones.RightUpperArm,
			HumanBodyBones.RightLowerArm
		};
		private static readonly HumanBodyBones[] RIGHT_WRIST_BONES = new [] { HumanBodyBones.RightHand };
		private static readonly HumanBodyBones[] RIGHT_FINGERS_BONES = new [] {
			HumanBodyBones.RightIndexProximal,
			HumanBodyBones.RightIndexIntermediate,
			HumanBodyBones.RightIndexDistal,
			HumanBodyBones.RightMiddleProximal,
			HumanBodyBones.RightMiddleIntermediate,
			HumanBodyBones.RightMiddleDistal,
			HumanBodyBones.RightRingProximal,
			HumanBodyBones.RightRingIntermediate,
			HumanBodyBones.RightRingDistal,
			HumanBodyBones.RightLittleProximal,
			HumanBodyBones.RightLittleIntermediate,
			HumanBodyBones.RightLittleDistal,
			HumanBodyBones.RightThumbProximal,
			HumanBodyBones.RightThumbIntermediate,
			HumanBodyBones.RightThumbDistal
		};
		
		private static readonly HumanBodyBones[] CHEST_BONES = new [] {
			HumanBodyBones.Chest,
			HumanBodyBones.UpperChest,
			HumanBodyBones.Spine
		};

		private static readonly HumanBodyBones[] HIPS_BONES = new [] { HumanBodyBones.Hips };
		private static readonly HumanBodyBones[] LEFT_HIP_BONES = new [] { HumanBodyBones.LeftUpperLeg };
		private static readonly HumanBodyBones[] LEFT_KNEE_BONES = new [] { HumanBodyBones.LeftLowerLeg };
		private static readonly HumanBodyBones[] LEFT_ANKLE_BONES = new [] { HumanBodyBones.LeftFoot };
		private static readonly HumanBodyBones[] RIGHT_HIP_BONES = new [] { HumanBodyBones.RightUpperLeg };
		private static readonly HumanBodyBones[] RIGHT_KNEE_BONES = new [] { HumanBodyBones.RightLowerLeg };
		private static readonly HumanBodyBones[] RIGHT_ANKLE_BONES = new [] { HumanBodyBones.RightFoot };
		private static readonly HumanBodyBones[] NONE_BONES = new HumanBodyBones[0];

		public static HumanBodyBones[] GetBones(int index) {
			return index switch {
				FACE => FACE_BONES,
				NECK => NECK_BONES,
				LEFT_ARM => LEFT_ARM_BONES,
				LEFT_WRIST => LEFT_WRIST_BONES,
				LEFT_FINGERS => LEFT_FINGERS_BONES,
				RIGHT_ARM => RIGHT_ARM_BONES,
				RIGHT_WRIST => RIGHT_WRIST_BONES,
				RIGHT_FINGERS => RIGHT_FINGERS_BONES,
				CHEST => CHEST_BONES,
				HIPS => HIPS_BONES,
				LEFT_HIP => LEFT_HIP_BONES,
				LEFT_KNEE => LEFT_KNEE_BONES,
				LEFT_ANKLE => LEFT_ANKLE_BONES,
				RIGHT_HIP => RIGHT_HIP_BONES,
				RIGHT_KNEE => RIGHT_KNEE_BONES,
				RIGHT_ANKLE => RIGHT_ANKLE_BONES,
				_ => NONE_BONES
			};
		}

		public static bool CanExternalModify(HumanBodyBones bone) {
			for (int i = 0; i < Count; i++) {
				// If the bone is not disabled we do not allow thiss
				if (Get(i)) {
					HumanBodyBones[] array = GetBones(i);
					foreach (HumanBodyBones item in array) {
						if (bone == item) {
							// The bone belonged to an array that is controlled by our software
							return false;
						}
					}
				}
			}

			return true;
		}

		private static readonly Quaternion DefaultLeftUpperArm = Quaternion.Euler(0, 0, 80);
		private static readonly Quaternion DefaultRightUpperArm = Quaternion.Euler(0, 0, -80);
		private static readonly Quaternion DefaultRot = Quaternion.identity;
		public static Quaternion GetDefaultRotation(HumanBodyBones bone) {
			return bone switch {
				HumanBodyBones.LeftUpperArm => DefaultLeftUpperArm,
				HumanBodyBones.RightUpperArm => DefaultRightUpperArm,
				_ => DefaultRot
			};
		}
	}
}
